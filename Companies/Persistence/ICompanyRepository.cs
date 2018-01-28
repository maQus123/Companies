namespace Companies.Persistence {

    using Companies.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyRepository {

        Task Add(Company company);

        Task<IEnumerable<Company>> GetAll();

        Task<Dictionary<int, string>> GetListOfCompanies();

        Task<Company> GetSingle(int id);

        Task<bool> IsUnique(string title, int id = 0);

        Task Update(Company existingCompany);

    }

}