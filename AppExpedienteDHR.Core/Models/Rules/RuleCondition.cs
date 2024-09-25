using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Models.Rules
{
    public class RuleCondition
    {
        public string Field { get; set; } // Ej: "Denunciante.Nombre"
        public string Operator { get; set; } // Ej: "Equals", "GreaterThan"
        public string Value { get; set; } // Valor contra el cual se evalúa
        public string LogicalOperator { get; set; } // "AND", "OR" (opcional)
    }
}
