namespace Companies.Controllers {

    using Companies.Models;
    using Companies.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CompanyController : Controller {

        private readonly ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository) {
            this.companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List() {
            var companies = await this.companyRepository.GetAll();
            return View(companies);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company) {
            if (ModelState.IsValid) {
                var isUniqueTitle = await this.companyRepository.IsUnique(company.Title);
                if (isUniqueTitle) {
                    await this.companyRepository.Add(company);
                    return RedirectToRoute("listCompanies");
                }
            }
            //TODO Fix bug when form is posted with empty AmountEmployee
            //TODO Display custom validation message
            return View(company);
        }

    }

}