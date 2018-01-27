namespace Companies.Models {

    public class Company {

        public int Id { get; private set; }

        public int AmountEmployees { get; set; }

        public string Branch { get; set; }

        public string Title { get; set; }

        public string Town { get; set; }

        public Company() {
            //nothing to do
        }

    }

}