namespace Companies.Persistence {

    using Companies.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyRepository {

        Task<IEnumerable<Company>> GetAll();

    }

}