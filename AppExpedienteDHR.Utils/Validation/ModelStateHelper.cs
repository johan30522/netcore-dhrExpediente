using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace AppExpedienteDHR.Utils.Validation
{
    public static class ModelStateHelper
    {
        public static void RemoveModelStateForObject(ModelStateDictionary modelState, string objectName)
        {
            var keys = modelState.Keys.Where(k => k.StartsWith(objectName + ".")).ToList();
            if (keys.Count == 0)
            {
                keys = modelState.Keys.Where(k => k.StartsWith(objectName )).ToList();
            }
            foreach (var key in keys)
            {
                modelState.Remove(key);
            }
        }
    }
}
