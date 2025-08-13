using NeuroEase.Core.Model.Entity;

namespace NeuroEase.Core.Repository
{
    public interface ICompaniesRepository
    {
        void Add(CompanyData company);
        IEnumerable<CompanyData> GetAll();
    }
}
