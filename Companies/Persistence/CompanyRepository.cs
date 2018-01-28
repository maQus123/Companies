namespace Companies.Persistence {

    using Companies.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyRepository : ICompanyRepository {

        private readonly DatabaseContext dbContext;

        public CompanyRepository(DatabaseContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task Add(Company company) {
            await this.dbContext.AddAsync(company);
            await this.dbContext.SaveChangesAsync();
            return;
        }

        public async Task<IEnumerable<Company>> GetAll() {
            var companies = await this.dbContext.Companies.OrderBy(c => c.Title).ToListAsync();
            return companies;
        }

        public async Task<Dictionary<int, string>> GetListOfCompanies() {
            var listOfCompanies = new Dictionary<int, string>();
            var companies = await this.GetAll();
            foreach (var company in companies) {
                listOfCompanies.Add(company.Id, company.Title);
            }
            return listOfCompanies;
        }

        public async Task<Company> GetSingle(int id) {
            var company = await this.dbContext.Companies.SingleOrDefaultAsync(c => c.Id == id);
            return company;
        }

        public async Task<bool> IsUnique(string title, int id = 0) {
            bool doesOccur;
            if (id == 0) {
                doesOccur = await this.dbContext.Companies.AnyAsync(p => p.Title.ToLowerInvariant() == title.ToLowerInvariant());
            } else {
                doesOccur = await this.dbContext.Companies.AnyAsync(p => p.Title.ToLowerInvariant() == title.ToLowerInvariant() && p.Id != id);
            }            
            return !doesOccur;
        }

        public async Task Update(Company existingCompany) {
            this.dbContext.Companies.Update(existingCompany);
            await this.dbContext.SaveChangesAsync();
            return;
        }

    }

}