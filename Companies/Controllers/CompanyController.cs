namespace Companies.Controllers {

    using Companies.Models;
    using Companies.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Threading.Tasks;

    public class CompanyController : Controller {

        private readonly ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository) {
            this.companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create() {
            await this.PopulateListOfParentCompanies();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company) {
            if (!ModelState.IsValid) {
                await this.PopulateListOfParentCompanies();
                return View(company);
            }
            var isUniqueTitle = await this.companyRepository.IsUnique(company.Title);
            if (!isUniqueTitle) {
                await this.PopulateListOfParentCompanies();
                ModelState.AddModelError(nameof(Company.Title), "Firmenname bereits vergeben.");
                return View(company);
            }
            if (company.ParentCompanyId.HasValue) {
                var parentCompany = await this.companyRepository.GetSingle(company.ParentCompanyId.Value);
                if (null != parentCompany) {
                    company.ParentCompany = parentCompany;
                }
            }
            await this.companyRepository.Add(company);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            var company = await this.companyRepository.GetSingle(id);
            if (null == company) {
                return NotFound();
            }
            await this.PopulateListOfParentCompanies(id);
            return View(company);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company editedCompany) {
            if (!ModelState.IsValid) {
                await this.PopulateListOfParentCompanies(id);
                return View(editedCompany);
            }
            var existingCompany = await this.companyRepository.GetSingle(id);
            if (null == existingCompany) {
                return NotFound();
            }
            if (existingCompany.Id != id) {
                return BadRequest();
            }
            var isTitleStillUnique = await this.companyRepository.IsUnique(editedCompany.Title, existingCompany.Id);
            if (!isTitleStillUnique) {
                await this.PopulateListOfParentCompanies(id);
                ModelState.AddModelError(nameof(Company.Title), "Firmenname bereits vergeben.");
                return View(editedCompany);
            }
            if (existingCompany.ParentCompanyId != editedCompany.ParentCompanyId) {
                if (editedCompany.ParentCompanyId.HasValue) {
                    var parentCompany = await this.companyRepository.GetSingle(editedCompany.ParentCompanyId.Value);
                    if (null != parentCompany) {
                        if (parentCompany.ParentCompanyId == existingCompany.Id) {
                            await this.PopulateListOfParentCompanies(id);
                            ModelState.AddModelError(nameof(Company.ParentCompanyId), "Zirkelbezug nicht zulässig.");
                            return View(editedCompany);
                        }
                        editedCompany.ParentCompany = parentCompany;
                    }
                }
            }
            existingCompany.UpdateFrom(editedCompany);
            await this.companyRepository.Update(existingCompany);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(ListViewModel listViewModel) {
            listViewModel.Companies = await this.companyRepository.GetAll(listViewModel.PageSize, listViewModel.CurrentPage, listViewModel.FilteredBranch, listViewModel.SearchText);
            listViewModel.CompaniesCount = await this.companyRepository.CountCompanies(listViewModel.FilteredBranch, listViewModel.SearchText);
            return View(listViewModel);
        }

        private async Task PopulateListOfParentCompanies(int excludedCompanyId = 0) {
            var parentCompanies = await this.companyRepository.GetParentCompanies(excludedCompanyId);
            ViewData["ParentCompanies"] = new SelectList(parentCompanies, "Key", "Value");
            return;
        }

    }

}