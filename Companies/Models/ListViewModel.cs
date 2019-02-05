namespace Companies.Models {

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;

    public class ListViewModel {

        private const int PAGE_SIZE = 5;

        public ICollection<Company> Companies { get; set; }

        public int CompaniesCount { get; set; }

        public Branch? FilteredBranch { get; set; }

        public string SearchText { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages {
            get {
                if (this.CompaniesCount == 0) {
                    return 1;
                } else {
                    return decimal.ToInt32(Math.Ceiling(decimal.Divide(this.CompaniesCount, this.PageSize)));
                }
            }
        }

        public bool ShowPrevious {
            get { return this.CurrentPage > 1; }
        }

        public bool ShowNext {
            get { return this.CurrentPage < this.TotalPages; }
        }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public ListViewModel() {
            this.CurrentPage = 1;
            this.PageSize = ListViewModel.PAGE_SIZE;
        }

    }

}