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

        public async Task<IEnumerable<Company>> GetAll() {
            var companies = await this.dbContext.Companies.OrderBy(p => p.Title).ToListAsync();
            return companies;
        }

    }

}