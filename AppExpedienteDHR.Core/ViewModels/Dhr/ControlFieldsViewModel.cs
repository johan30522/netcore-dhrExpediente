using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ViewModels.Dhr
{
    public class ControlFieldsViewModel
    {
        public bool IsEdit { get; set; } = false;
        public int? LockedRecordId { get; set; }
        public int? FlowHeaderWfId { get; set; }
        public int? FlowWfId { get; set; }
        public string? FlowWfName { get; set; }
        public int? StateWfId { get; set; }
        public string? StateWfName { get; set; }
        public DateTime? CreatedDate { get; set; }




    }
}
