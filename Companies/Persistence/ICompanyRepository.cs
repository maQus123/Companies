namespace Companies.Persistence {

    using Companies.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyRepository {

        Task Add(Company company);

        Task<IEnumerable<Company>> GetAll();

        int GetHierarchicalLevel(Company company);

        Task<Dictionary<int, string>> GetParentCompanies();

        Task<Dictionary<int, string>> GetParentCompanies(int excludedCompanyId);

        Company GetSingle(int id);

        Task<Company> GetSingle(int? id);

        Task<bool> IsUnique(string title);

        Task<bool> IsUnique(string title, int id);

        Task Update(Company existingCompany);
        
    }

}