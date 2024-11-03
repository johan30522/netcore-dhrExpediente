using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.EntityContracts
{
    public interface ISoftDeletable
    {
        bool? IsDeleted { get; set; }
    }
}
