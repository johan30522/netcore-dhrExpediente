using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Workflow
{
    public interface IRequestWfService
    {
        Task<object> GetCompleteRequest(int id, string requestType);
    }
}
