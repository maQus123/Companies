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
            var listofCompanies = await this.companyRepository.GetListOfCompanies();
            ViewData["ParentCompanies"] = new SelectList(listofCompanies, "Key", "Value");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company) {
            if (!ModelState.IsValid) {
                return View(company);
            }
            var isUniqueTitle = await this.companyRepository.IsUnique(company.Title);
            if (!isUniqueTitle) {
                ModelState.AddModelError(nameof(Company.Title), "Firmenname bereits vergeben.");
                return View(company);
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
            return View(company);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company editedCompany) {
            if (!ModelState.IsValid) {
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
                ModelState.AddModelError(nameof(Company.Title), "Firmenname bereits vergeben.");
                return View(editedCompany);
            }
            existingCompany.UpdateFrom(editedCompany);
            await this.companyRepository.Update(existingCompany);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List() {
            var companies = await this.companyRepository.GetAll();
            return View(companies);
        }

    }

}