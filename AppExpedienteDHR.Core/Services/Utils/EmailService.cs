﻿using AppExpedienteDHR.Core.Models;
using AppExpedienteDHR.Core.ServiceContracts.Utils;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Serilog;

namespace AppExpedienteDHR.Core.Services.Utils
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false, string cc = null, string bcc = null)
        {
            ValidateEmailSettings();

            using var client = new SmtpClient(_emailSettings.SMTPServer, _emailSettings.SMTPPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderDisplayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            AddEmailAddresses(to, mailMessage.To);
            AddEmailAddresses(cc, mailMessage.CC);
            AddEmailAddresses(bcc, mailMessage.Bcc);

            try
            {
                _logger.Information("Intentando enviar correo a {Recipients} con asunto {Subject}", to, subject);
                for (int attempt = 0; attempt < 3; attempt++)
                {
                    try
                    {
                        await client.SendMailAsync(mailMessage);
                        _logger.Information("Correo enviado exitosamente a {Recipients}", to);
                        return;
                    }
                    catch (SmtpException ex) when (attempt < 2)
                    {
                        _logger.Warning(ex, "Error temporal al enviar correo (intento {Attempt})", attempt + 1);
                        await Task.Delay(TimeSpan.FromSeconds(2 * (attempt + 1)));
                    }
                }

                _logger.Error("No se pudo enviar el correo a {Recipients} después de múltiples intentos", to);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado al enviar correo a {Recipients}", to);
            }
        }

        public async Task SendEmailWithAttachmentAsync(
            string to,
            string subject,
            string body,
            byte[]? attachment = null,
            string? attachmentName = null,
            bool isHtml = false,
            string? cc = null,
            string? bcc = null)
        {
            ValidateEmailSettings();

            using var client = new SmtpClient(_emailSettings.SMTPServer, _emailSettings.SMTPPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderDisplayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            AddEmailAddresses(to, mailMessage.To);
            AddEmailAddresses(cc, mailMessage.CC);
            AddEmailAddresses(bcc, mailMessage.Bcc);

            // Manejo opcional de adjuntos
            if (attachment != null && attachment.Length > 0)
            {
                if (attachment.Length > 10 * 1024 * 1024) // 10 MB límite
                {
                    _logger.Warning("El adjunto '{AttachmentName}' excede el tamaño permitido de 10 MB. No se incluirá en el correo.", attachmentName ?? "SinNombre");
                }
                else
                {
                    try
                    {
                        var attachmentItem = new Attachment(new MemoryStream(attachment), attachmentName ?? "Adjunto");
                        mailMessage.Attachments.Add(attachmentItem);
                        _logger.Information("Adjunto agregado: {AttachmentName}", attachmentName ?? "SinNombre");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error al agregar el adjunto '{AttachmentName}'", attachmentName ?? "SinNombre");
                    }
                }
            }
            else
            {
                _logger.Information("No se proporcionó adjunto o es vacío.");
            }

            // Enviar el correo con reintentos
            try
            {
                _logger.Information("Intentando enviar correo a {Recipients} con asunto {Subject}", to, subject);
                for (int attempt = 0; attempt < 3; attempt++)
                {
                    try
                    {
                        await client.SendMailAsync(mailMessage);
                        _logger.Information("Correo enviado exitosamente a {Recipients}", to);
                        return;
                    }
                    catch (SmtpException ex) when (attempt < 2)
                    {
                        _logger.Warning(ex, "Error temporal al enviar correo (intento {Attempt})", attempt + 1);
                        await Task.Delay(TimeSpan.FromSeconds(2 * (attempt + 1))); // Retraso incremental
                    }
                }

                _logger.Error("No se pudo enviar el correo a {Recipients} después de múltiples intentos", to);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inesperado al enviar correo a {Recipients}", to);
            }
        }

        public async Task SendEmailFromTemplateAsync(string to, string subject, string htmlContent, string cc = null, string bcc = null)
        {
            await SendEmailAsync(to, subject, htmlContent, true, cc, bcc);
        }

        public void ValidateEmailSettings()
        { 
            if (string.IsNullOrEmpty(_emailSettings.SMTPServer) ||
                string.IsNullOrEmpty(_emailSettings.SenderEmail) ||
                string.IsNullOrEmpty(_emailSettings.SenderPassword))
            {
                throw new InvalidOperationException("SMTP settings are not properly configured.");
            }
        }

        private void AddEmailAddresses(string addresses, MailAddressCollection collection)
        {
            if (!string.IsNullOrEmpty(addresses))
            {
                // divide las direcciones de correo electrónico por ; o ,
                var recipients = addresses.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var recipient in recipients)
                {
                    collection.Add(recipient.Trim());
                }
            }
        }
    }
}
