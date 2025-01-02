
namespace AppExpedienteDHR.Core.ServiceContracts.Admin
{
    public interface ITemplateRenderer
    {
        Task<string> RenderAsync(string template, object model);

    }
}
