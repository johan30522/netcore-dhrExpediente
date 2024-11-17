
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using Serilog;


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using AppExpedienteDHR.Core.Domain.Entities.General;
using AppExpedienteDHR.Core.Models;
using Microsoft.Extensions.Options;

namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class AdjuntoService : IAdjuntoService
    {
        private readonly IContainerWork _unitOfWork;
        private readonly string _rutaBaseArchivos;
        private readonly ILogger _logger;


        public AdjuntoService(IContainerWork unitOfWork, ILogger logger, IOptions<FileStorageOptions> fileStorageOptions)
        {
            _unitOfWork = unitOfWork;
            _rutaBaseArchivos = fileStorageOptions.Value.Path;
            _logger = logger;
        }

        public async Task<int> CrearAnexoAsync(IFormFile archivo)
        {
            var nombreUnico = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

            var anexo = new Adjunto
            {
                NombreOriginal = archivo.FileName,
                Ruta = nombreUnico,
                FechaSubida = DateTime.Now
            };

            var rutaCompleta = Path.Combine(_rutaBaseArchivos, nombreUnico);

            // Guardar el archivo en el sistema de archivos
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Guardar el anexo en la base de datos
            await _unitOfWork.Adjunto.Add(anexo);
            await _unitOfWork.Save();

            return anexo.Id;
        }

        public async Task<IEnumerable<int>> CrearAnexosAsync(IEnumerable<IFormFile> archivos)
        {
            var idsAnexos = new List<int>();

            foreach (var archivo in archivos)
            {
                var idAnexo = await CrearAnexoAsync(archivo);
                idsAnexos.Add(idAnexo);
            }

            return idsAnexos;
        }

        public async Task<byte[]> DescargarArchivoAsync(string ruta)
        {
            // Buscar el anexo por la ruta
            var anexo = await _unitOfWork.Adjunto.ObtenerPorRuta(ruta);

            if (anexo == null)
            {
                throw new FileNotFoundException("El archivo no existe.");
            }

            var rutaCompleta = Path.Combine(_rutaBaseArchivos, anexo.Ruta);

            // Leer el archivo físico desde el disco
            if (File.Exists(rutaCompleta))
            {
                return await File.ReadAllBytesAsync(rutaCompleta);
            }
            else
            {
                throw new FileNotFoundException("El archivo no fue encontrado en el servidor.");
            }
        }



        public async Task EliminarArchivoAsync(int anexoId)
        {
            var anexo = await _unitOfWork.Adjunto.Get(anexoId);

            if (anexo == null)
            {
                throw new Exception("El archivo no fue encontrado.");
            }

            var rutaCompleta = Path.Combine(_rutaBaseArchivos, anexo.Ruta);

            // Eliminar archivo del sistema de archivos
            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }

            // Eliminar la referencia en la base de datos
            await _unitOfWork.Adjunto.Remove(anexo.Id);
            await _unitOfWork.Save();
        }

        public Task<Adjunto> GetAnexo(int id)
        {
            var anexo = _unitOfWork.Adjunto.Get(id);
            if (anexo == null)
            {
                return null;
            }
            return anexo;

        }
    }
}

