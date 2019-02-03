namespace Companies.Models {

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    public class ListViewModel {

        private const int PAGE_SIZE = 2;

        public ICollection<Company> Companies { get; set; }

        public int CompaniesCount { get; set; }

        public Branch? FilteredBranch { get; set; }

        public string SearchText { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages {
            get { return decimal.ToInt32(Math.Ceiling(decimal.Divide(this.CompaniesCount, this.PageSize))); }
        }

        public ListViewModel() {
            this.CurrentPage = 1;
            this.PageSize = ListViewModel.PAGE_SIZE;
        }

    }

}