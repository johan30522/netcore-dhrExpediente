using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Admin
{
    public class DescriptorListViewModel
    {
        public int EventoId { get; set; }
        public IEnumerable<DescriptorViewModel> Descriptores { get; set; }
    }
}
