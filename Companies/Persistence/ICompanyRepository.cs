namespace Companies.Persistence {

    using Companies.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyRepository {

        Task Add(Company company);

        Task<IEnumerable<Company>> GetAll();

        Task<bool> IsUnique(string title);

    }

}