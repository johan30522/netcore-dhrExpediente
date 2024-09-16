using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Dhr
{
    public interface IAdjuntoService
    {
        Task<int> CrearAnexoAsync(IFormFile archivo);
        Task<IEnumerable<int>> CrearAnexosAsync(IEnumerable<IFormFile> archivos);
        Task<byte[]> DescargarArchivoAsync(string ruta);
        Task EliminarArchivoAsync(int anexoId);
    }
}
