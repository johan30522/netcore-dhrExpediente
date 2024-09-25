using Newtonsoft.Json;
using AppExpedienteDHR.Core.Models.Rules;

namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class RuleEvaluator
    {
        public bool Evaluate<T>(T entity, string ruleJson)
        {
            var ruleSet = JsonConvert.DeserializeObject<RuleSet>(ruleJson); // Deserializar el JSON

            bool result = true;
            foreach (var condition in ruleSet.Conditions)
            {
                var fieldValue = GetPropertyValue(entity, condition.Field); // Obtener el valor del campo usando reflexión
                var isConditionMet = ApplyOperator(fieldValue, condition.Operator, condition.Value); // Aplicar el operador

                if (condition.LogicalOperator == "AND")
                {
                    result = result && isConditionMet;
                }
                else if (condition.LogicalOperator == "OR")
                {
                    result = result || isConditionMet;
                }
                else
                {
                    result = isConditionMet; // Si no hay operador lógico, solo evaluamos esta condición
                }

                if (!result && condition.LogicalOperator == "AND") break; // Si una condición "AND" falla, podemos salir
            }

            return result;
        }

        // Obtener el valor de una propiedad o campo dinámicamente usando Reflection
        private object GetPropertyValue<T>(T entity, string propertyName)
        {
            string[] propertyParts = propertyName.Split('.');
            object currentValue = entity;

            foreach (var part in propertyParts)
            {
                if (currentValue == null) return null;

                var propertyInfo = currentValue.GetType().GetProperty(part);
                if (propertyInfo == null) return null;

                currentValue = propertyInfo.GetValue(currentValue, null);
            }

            return currentValue;
        }

        // Aplicar operadores de comparación
        private bool ApplyOperator(object fieldValue, string operatorType, string comparisonValue)
        {
            switch (operatorType)
            {
                case "Equals":
                    return fieldValue != null && fieldValue.ToString() == comparisonValue;
                case "NotEquals":
                    return fieldValue == null || fieldValue.ToString() != comparisonValue;
                case "GreaterThan":
                    return Convert.ToDecimal(fieldValue) > Convert.ToDecimal(comparisonValue);
                case "LessThan":
                    return Convert.ToDecimal(fieldValue) < Convert.ToDecimal(comparisonValue);
                case "Contains":
                    return fieldValue != null && fieldValue.ToString().Contains(comparisonValue);
                default:
                    throw new InvalidOperationException($"Operador no soportado: {operatorType}");
            }
        }
    }
}
