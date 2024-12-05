using AppExpedienteDHR.Core.ServiceContracts.Admin;
using Fluid;
using Fluid.Values;
using Serilog;
using System.Net;

namespace AppExpedienteDHR.Core.Services.Admin
{
    public class TemplateRenderer : ITemplateRenderer
    {
        private readonly TemplateOptions _options;
        private readonly ILogger _logger;

        public TemplateRenderer(ILogger logger)
        {
            _options = new TemplateOptions();
            _logger = logger;
            // Agrega filtros personalizados si los necesitas
            _options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();

            RegisterCustomFilters();
        }


        public async Task<string> RenderAsync(string template, IDictionary<string, object> model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template))
                    throw new ArgumentNullException(nameof(template));

                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                // Decodificar la plantilla
                var decodedTemplate = WebUtility.HtmlDecode(template);
                _logger.Information("Plantilla decodificada: {Template}", decodedTemplate);


                var parser = new FluidParser();
                if (!parser.TryParse(decodedTemplate, out var fluidTemplate, out var error))
                {
                    throw new Exception($"Error al analizar la plantilla: {error}");
                }


                var context = new TemplateContext(model, _options);
                _logger.Information("Modelo proporcionado: {@Model}", model);

                var returnValueStr = fluidTemplate.Render(context);
                _logger.Information("Resultado renderizado: {Result}", returnValueStr);

                return await Task.FromResult(returnValueStr);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al renderizar la plantilla");
                throw new Exception("Error al renderizar la plantilla", ex);
            }

        }

        /// <summary>
        /// Registra todos los filtros personalizados para las plantillas Fluid.
        /// </summary>
        private void RegisterCustomFilters()
        {
            _options.Filters.AddFilter("format_date", (input, arguments, context) =>
            {
                try
                {
                    // Convertir el valor de entrada en un DateTime o DateTimeOffset
                    var rawDate = input.ToObjectValue();

                    DateTime date;
                    if (rawDate is DateTime dateValue)
                    {
                        date = dateValue;
                    }
                    else if (rawDate is DateTimeOffset dateOffset)
                    {
                        date = dateOffset.DateTime; // Convertir DateTimeOffset a DateTime
                    }
                    else
                    {
                        _logger.Warning("El valor proporcionado no es una fecha válida.");
                        return FluidValue.Create(null, context.Options);
                    }

                    // Obtener el formato del primer argumento
                    var format = arguments.At(0).ToStringValue();

                    // Validar que el formato no sea nulo o vacío
                    if (string.IsNullOrWhiteSpace(format))
                    {
                        _logger.Warning("El formato proporcionado en format_date está vacío o no es válido.");
                        return FluidValue.Create("Formato inválido", context.Options);
                    }

                    // Formatear la fecha y devolverla
                    var formattedDate = date.ToString(format);
                    return FluidValue.Create(formattedDate, context.Options);
                }
                catch (Exception ex)
                {
                    // Capturar cualquier excepción y devolver un mensaje de error
                    _logger.Error(ex, "Error al procesar el filtro format_date");
                    return FluidValue.Create($"Error: {ex.Message}", context.Options);
                }
            });

            _options.Filters.AddFilter("uppercase", (input, arguments, context) =>
            {
                return FluidValue.Create(input.ToStringValue().ToUpper(), context.Options);
            });

            _options.Filters.AddFilter("lowercase", (input, arguments, context) =>
            {
                return FluidValue.Create(input.ToStringValue().ToLower(), context.Options);
            });

            _options.Filters.AddFilter("truncate", (input, arguments, context) =>
            {
                var text = input.ToStringValue();
                var length = (int)arguments.At(0).ToNumberValue();
                var result = text.Length > length ? text.Substring(0, length) + "..." : text;
                return FluidValue.Create(result, context.Options);
            });

            _options.Filters.AddFilter("format_number", (input, arguments, context) =>
            {
                /// <summary>
                /// Filtro personalizado para formatear números en plantillas Fluid.
                /// 
                /// Permite aplicar formatos estándar de C#, como:
                /// - "D<n>": Dígitos con ceros a la izquierda (e.g., "D6" → 000123).
                /// - "N<n>": Número con separadores de miles y decimales (e.g., "N2" → 1,234.56).
                /// - "C<n>": Formato de moneda basado en configuración regional (e.g., "C" → $1,234.56).
                /// - "P<n>": Formato de porcentaje (e.g., "P2" → 12.34%).
                /// - "E<n>": Notación exponencial (e.g., "E2" → 1.23E+004).
                /// - Formatos personalizados (e.g., "0,0.00" → 123,456.79).
                /// 
                /// Manejo de errores:
                /// - Si el formato o el valor no es válido, devuelve un mensaje de error en lugar de interrumpir el renderizado.
                /// 
                /// Ejemplo de uso en una plantilla:
                /// <p>Su número de expediente es: {{ Expediente.Id | format_number: "D6" }}</p>
                /// 
                /// Resultado (si Expediente.Id = 123): <p>Su número de expediente es: 000123</p>
                /// </summary>
                try
                {
                    var number = input.ToNumberValue(); // Obtener el número
                    var format = arguments.At(0).ToStringValue(); // Obtener el formato

                    // Validar si el número es un entero
                    if (number % 1 == 0) // Es un número entero
                    {
                        var intValue = Convert.ToInt32(number);
                        return FluidValue.Create(intValue.ToString(format), context.Options);
                    }
                    else // Es un número decimal
                    {
                        return FluidValue.Create(number.ToString(format), context.Options);
                    }
                }
                catch (FormatException ex)
                {
                    return FluidValue.Create($"Error: Formato inválido '{ex.Message}'", context.Options);
                }
                catch (Exception ex)
                {
                    return FluidValue.Create($"Error inesperado: {ex.Message}", context.Options);
                }
            });

            _options.Filters.AddFilter("default", (input, arguments, context) =>
            {
                var defaultValue = arguments.At(0).ToStringValue();
                var result = input.IsNil() ? defaultValue : input.ToStringValue();
                return FluidValue.Create(result, context.Options);
            });

            _options.Filters.AddFilter("capitalize", (input, arguments, context) =>
            {
                var text = input.ToStringValue();
                var result = char.ToUpper(text[0]) + text.Substring(1);
                return FluidValue.Create(result, context.Options);
            });

            _options.Filters.AddFilter("trim", (input, arguments, context) =>
            {
                var result = input.ToStringValue().Trim();
                return FluidValue.Create(result, context.Options);
            });

            _options.Filters.AddFilter("initials", (input, arguments, context) =>
            {
                var words = input.ToStringValue().Split(' ');
                var result = string.Join("", words.Select(w => w[0]));
                return FluidValue.Create(result, context.Options);
            });

            _options.Filters.AddFilter("url", (input, arguments, context) =>
            {
                try
                {
                    var baseUrl = arguments.At(0).ToStringValue(); // Obtener la URL base del argumento
                    var text = input.ToStringValue(); // Obtener el texto de entrada

                    // Construir la URL completa
                    var url = $"{baseUrl}/{text}";

                    // Generar el enlace HTML
                    var link = $"<a href=\"{url}\" target=\"_blank\">Abrir enlace</a>";

                    return FluidValue.Create(link, context.Options);
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, retornar un mensaje indicando el problema
                    return FluidValue.Create($"Error generando URL: {ex.Message}", context.Options);
                }
            });

            _options.Filters.AddFilter("round", (input, arguments, context) =>
            {
                var number = input.ToNumberValue();
                var decimals = (int)arguments.At(0).ToNumberValue();
                var result = Math.Round(number, decimals);
                return FluidValue.Create(result, context.Options);
            });

            _options.Filters.AddFilter("to_json", (input, arguments, context) =>
            {
                var json = System.Text.Json.JsonSerializer.Serialize(input.ToObjectValue());
                return FluidValue.Create(json, context.Options);
            });

            _options.Filters.AddFilter("hash", (input, arguments, context) =>
            {
                var text = input.ToStringValue();
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
                    var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                    return FluidValue.Create(hash, context.Options);
                }
            });

            _logger.Information("Filtros personalizados registrados exitosamente.");
        }


    }
}
