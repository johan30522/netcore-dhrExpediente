using AppExpedienteDHR.Core.ServiceContracts.Admin;
using Fluid;

namespace AppExpedienteDHR.Core.Services.Admin
{
    public class TemplateRenderer: ITemplateRenderer
    {
        private readonly TemplateOptions _options;

        public TemplateRenderer()
        {
            _options = new TemplateOptions();
            // Agrega filtros personalizados si los necesitas
        }

        public async Task<string> RenderAsync(string template, IDictionary<string, object> model)
        {
            if (string.IsNullOrWhiteSpace(template))
                throw new ArgumentNullException(nameof(template));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var parser = new FluidParser();
            if (!parser.TryParse(template, out var fluidTemplate, out var error))
            {
                throw new Exception($"Error al analizar la plantilla: {error}");
            }

            var context = new TemplateContext(model, _options);
            return await Task.FromResult(fluidTemplate.Render(context));
        }


    }
}
