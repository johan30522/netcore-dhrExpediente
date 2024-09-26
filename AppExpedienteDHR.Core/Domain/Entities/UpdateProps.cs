using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities
{
    public class UpdateProps
    {
        public DateTime UpdatedAt { get; set; }
        [StringLength(450)]
        public string UpdatedById { get; set; }
    }
}
