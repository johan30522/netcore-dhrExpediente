using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Utils.Services
{
    public class ExampleDataGenerator
    {
        public static object GenerateExampleData(Type type, int depth = 2)
        {
            if (depth == 0 || type == null) return null;

            var instance = Activator.CreateInstance(type);

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanWrite) continue;

                var propertyType = property.PropertyType;

                // Manejo de tipos simples
                if (propertyType == typeof(string))
                    property.SetValue(instance, $"Ejemplo de {property.Name}");
                else if (propertyType == typeof(int) || propertyType == typeof(int?))
                    property.SetValue(instance, 123);
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                    property.SetValue(instance, true);
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                    property.SetValue(instance, DateTime.Now);
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    // Crear colecciones con un solo elemento de ejemplo
                    var itemType = propertyType.GetGenericArguments()[0];
                    var listType = typeof(List<>).MakeGenericType(itemType);
                    var listInstance = Activator.CreateInstance(listType) as IList<object>;

                    listInstance?.Add(GenerateExampleData(itemType, depth - 1));
                    property.SetValue(instance, listInstance);
                }
                else if (!propertyType.IsPrimitive && !propertyType.IsEnum && propertyType != typeof(object))
                {
                    // Manejo de propiedades complejas (objetos anidados)
                    property.SetValue(instance, GenerateExampleData(propertyType, depth - 1));
                }
            }

            return instance;
        }
    }
}
