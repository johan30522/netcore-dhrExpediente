using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.Entities
{
    public class CommonProps
    {
        [Key]
        public int Id { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
