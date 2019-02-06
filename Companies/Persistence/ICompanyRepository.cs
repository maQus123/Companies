namespace Companies.Persistence {

    using Companies.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyRepository {

        Task Add(Company company);

        Task<int> CountCompanies(Branch? branch, string text = "");

        Task<ICollection<Company>> GetAll(int pageSize, int currentPage, Branch? branch, string textContains = "", string sortBy = "");
        
        Task<Dictionary<int, string>> GetParentCompanies(int excludedCompanyId);

        Task<Company> GetSingle(int id);

        Task<bool> IsUnique(string title, int ownCompanyId = 0);

        Task Update(Company existingCompany);

        void Remove(Company company);
                
    }

}