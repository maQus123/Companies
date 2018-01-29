namespace Companies.Models {

    using System;
    using System.ComponentModel.DataAnnotations;

    public enum Branch {

        [Display(Name = "Fleischverarbeitung")]
        Fleischverarbeitung = 1,

        [Display(Name = "Garten- und Landschaftsbau")]
        GartenLandschaftsbau = 2,

        [Display(Name = "IT-Dienstleistungen")]
        ITDienstleistungen = 3,

        [Display(Name = "Luft- und Raumfahrttechnik")]
        LuftRaumfahrttechnik = 4,

        [Display(Name = "Unterhaltungselektronik")]
        Unterhaltungselektronik = 5

    }

    public static class Extensions {

        public static string DisplayName(this Enum @enum) {
            var displayName = string.Empty;
            var fields = @enum.GetType().GetFields();
            foreach (var field in fields) {
                var displayNameAttribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
                if (displayNameAttribute != null && field.Name.Equals(@enum.ToString(), StringComparison.InvariantCultureIgnoreCase)) {
                    displayName = displayNameAttribute.Name;
                    break;
                }
            }
            return displayName;
        }

    }

}