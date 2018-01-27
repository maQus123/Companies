namespace Companies.Controllers {

    using Companies.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : Controller {

        private readonly ICompanyRepository companyRepository;

        public HomeController(ICompanyRepository companyRepository) {
            this.companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            var companies = await this.companyRepository.GetAll();
            return View(companies);
        }

    }

}