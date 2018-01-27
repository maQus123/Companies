namespace Companies.Persistence {

    using Companies.Models;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext {

        public DbSet<Company> Companies { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            //nothing to do
        }

    }

}