using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class DenunciaItemListViewModel
    {
        public int Id { get; set; }
        public string DenuncianteNombre { get; set; }
        public string DetalleDenuncia { get; set; }
        public string Petitoria { get; set; }
        public DateTime FechaDenuncia { get; set; }
    }
}
