namespace Companies.Models {

    using System;
    using System.ComponentModel;

    public enum Branch {

        [Description("Fleischverarbeitung")]
        Fleischverarbeitung = 1,

        [Description("Garten- und Landschaftsbau")]
        GartenLandschaftsbau = 2,

        [Description("IT-Dienstleistungen")]
        ITDienstleistungen = 3,

        [Description("Luft- und Raumfahrttechnik")]
        LuftRaumfahrttechnik = 4,

        [Description("Unterhaltungselektronik")]
        Unterhaltungselektronik = 5

    }

    public static class Extensions {

        public static string Description(this Enum @enum) {
            var description = string.Empty;
            var fields = @enum.GetType().GetFields();
            foreach (var field in fields) {
                var descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (descriptionAttribute != null && field.Name.Equals(@enum.ToString(), StringComparison.InvariantCultureIgnoreCase)) {
                    description = descriptionAttribute.Description;
                    break;
                }
            }
            return description;
        }

    }

}