using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace SM.Application.Extensions
{
    public static class EnumExtensios
    {
        public static List<SelectListItem> GetDescriptionEnum(Array valuesEnum)
        {
            var listOfEnum = new List<SelectListItem>();
            if (valuesEnum == null)
            {
                throw new ArgumentException("Parâmetros inválidos. Valores Enum não deve ser nulo.");
            }

            foreach (dynamic valor in valuesEnum)
            {
                var _texto = valor.GetType().GetField(valor.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(_texto, typeof(DescriptionAttribute));
                listOfEnum.Add(new SelectListItem
                {
                    Text = attribute == null ? valor.ToString() : attribute.Description,
                    Value = valor.ToString()
                });
            }
            return listOfEnum;
        }
    }
}
