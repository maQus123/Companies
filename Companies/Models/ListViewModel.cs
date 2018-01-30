namespace Companies.Models {

    using System.Collections.Generic;

    public class ListViewModel {

        public IEnumerable<Company> Companies { get; set; }

        public Branch? FilteredBranch { get; set; }

        public string TextContains { get; set; }

        public ListViewModel() {
            //nothing to do
        }

    }

}