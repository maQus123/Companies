namespace Companies.Persistence {

    using Companies.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
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

        public async Task<int> CountCompanies(Branch? branch, string searchText = "") {
            var companies = await this.GetAll(branch, searchText, string.Empty);
            return companies.Count;
        }

        public async Task<ICollection<Company>> GetAll(int pageSize, int currentPage, Branch? branch, string searchText = "", string sortBy = "") {
            var companies = await this.GetAll(branch, searchText, sortBy);
            companies = companies.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return companies;
        }

        public async Task<Dictionary<int, string>> GetParentCompanies(int excludedCompanyId) {
            var parentCompanies = new Dictionary<int, string>();
            var companies = await this.dbContext.Companies.ToListAsync();
            foreach (var company in companies) {
                if (excludedCompanyId != company.Id) {
                    parentCompanies.Add(company.Id, company.Title);
                }
            }
            return parentCompanies;
        }

        public async Task<Company> GetSingle(int id) {
            var company = await this.dbContext.Companies.SingleOrDefaultAsync(c => c.Id == id);
            return company;
        }

        public async Task<bool> IsUnique(string title, int ownCompanyId = 0) {
            var doesOccur = await this.dbContext.Companies.AnyAsync(p => p.Title.ToLowerInvariant() == title.ToLowerInvariant() && p.Id != ownCompanyId);
            return !doesOccur;
        }

        public void Remove(Company company) {
            this.dbContext.Remove(company);
            this.dbContext.SaveChanges();
            return;
        }

        public async Task Update(Company existingCompany) {
            this.dbContext.Companies.Update(existingCompany);
            await this.dbContext.SaveChangesAsync();
            return;
        }

        private async Task<ICollection<Company>> GetAll(Branch? branch, string searchText = "", string sortBy = "") {
            var companies = this.dbContext.Companies.AsQueryable();
            if (null != branch) {
                companies = companies.Where(c => c.Branch == branch);
            }
            if (!string.IsNullOrEmpty(searchText)) {
                searchText = searchText.ToLowerInvariant();
                companies = companies.Where(c => 
                    (c.Title != null && c.Title.ToLowerInvariant().Contains(searchText)) || 
                    (c.City != null && c.City.ToLowerInvariant().Contains(searchText)) ||
                    (c.ParentCompany != null && c.ParentCompany.Title != null && c.ParentCompany.Title.ToLowerInvariant().Contains(searchText)));
            }
            if (!string.IsNullOrEmpty(sortBy)) {
                companies = companies.OrderBy(sortBy);
            }
            return await companies.ToListAsync();
        }

    }

}