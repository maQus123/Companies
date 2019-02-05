namespace Companies.Models {

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Company {

        [Range(1, 1000000, ErrorMessage = "Anzahl Mitarbeiter muss zwischen 1 und 1.000.000 liegen.")]
        public int? AmountEmployees { get; set; }

        [Required(ErrorMessage = "Branche ist ein Pflichtfeld.")]
        public Branch Branch { get; set; }

        public int Id { get; private set; }

        [NotMapped]
        public int HierarchicalLevel {
            get { return this.GetHierarchicalLevel(); }
        }

        public int? ParentCompanyId { get; set; }

        public Company ParentCompany { get; set; }

        [Required(ErrorMessage = "Firmenname ist ein Pflichtfeld.")]
        [StringLength(50, ErrorMessage = "Firmenname mit max. 50 Zeichen.")]
        [RegularExpression(@"^[a-zA-Z0-9'\säÄöÖüÜß.-]*$", ErrorMessage = "Firmenname darf nur Zeichen aus dem deutschen Alphabet enthalten.")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Stadt mit max. 50 Zeichen.")]
        [RegularExpression(@"^[a-zA-Z0-9'\s-äÄöÖüÜß.-]*$", ErrorMessage = "Stadt darf nur Zeichen aus dem deutschen Alphabet enthalten.")]
        public string City { get; set; }

        public Company() {
            //nothing to do
        }

        private int GetHierarchicalLevel() {
            var level = 0;
            if (null != this.ParentCompany) {
                level++;
                level += this.ParentCompany.GetHierarchicalLevel();
            }
            return level;
        }

        public void UpdateFrom(Company company) {
            this.AmountEmployees = company.AmountEmployees;
            this.Branch = company.Branch;
            this.ParentCompany = company.ParentCompany;
            this.Title = company.Title;
            this.City = company.City;
            return;
        }

    }

}