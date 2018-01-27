namespace Companies.Models {

    using System.ComponentModel.DataAnnotations;

    public class Company {

        [Range(0, 1000000)]
        public int AmountEmployees { get; set; }

        [Required]
        public Branch Branch { get; set; }

        public int Id { get; private set; }

        [Required, StringLength(50), RegularExpression(@"^[a-zA-Z'\säÄöÖüÜß]*$")]
        public string Title { get; set; }

        [StringLength(50), RegularExpression(@"^[a-zA-Z'\s-äÄöÖüÜß]*$")]
        public string Town { get; set; }

        public Company() {
            //nothing to do
        }

    }

}