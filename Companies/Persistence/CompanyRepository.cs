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

        public async Task<IEnumerable<Company>> GetAll(Branch? branch, string textContains = "") {
            IEnumerable<Company> companies;
            //TODO Add textContains to LINQ Query
            if (null != branch) {
                companies = await this.dbContext.Companies.Where(c => c.Branch == branch).OrderBy(c => c.Title).ToListAsync();
            } else {
                companies = await this.GetAll();
            }
            return companies;
        }

        public int GetHierarchicalLevel(Company company) {
            var level = 0;
            if (null != company.ParentCompany) {
                level++;
                level += this.GetHierarchicalLevel(company.ParentCompany);
            }
            return level;
        }

        public async Task<Dictionary<int, string>> GetParentCompanies() {
            return await this.GetParentCompanies(0);
        }

        public async Task<Dictionary<int, string>> GetParentCompanies(int excludedCompanyId) {
            var parentCompanies = new Dictionary<int, string>();
            var companies = await this.GetAll();
            foreach (var company in companies) {
                if (excludedCompanyId != company.Id) {
                    parentCompanies.Add(company.Id, company.Title);
                }
            }
            return parentCompanies;
        }

        public Company GetSingle(int id) {
            var company = this.dbContext.Companies.SingleOrDefault(c => c.Id == id);
            return company;
        }

        public async Task<Company> GetSingle(int? id) {
            var company = await this.dbContext.Companies.SingleOrDefaultAsync(c => c.Id == id);
            return company;
        }

        public async Task<bool> IsUnique(string title) {
            var doesOccur = await this.dbContext.Companies.AnyAsync(p => p.Title.ToLowerInvariant() == title.ToLowerInvariant());
            return !doesOccur;
        }

        public async Task<bool> IsUnique(string title, int idOfOwnCompany) {
            var doesOccur = await this.dbContext.Companies.AnyAsync(p => p.Title.ToLowerInvariant() == title.ToLowerInvariant() && p.Id != idOfOwnCompany);
            return !doesOccur;
        }

        public async Task Update(Company existingCompany) {
            this.dbContext.Companies.Update(existingCompany);
            await this.dbContext.SaveChangesAsync();
            return;
        }

    }

}