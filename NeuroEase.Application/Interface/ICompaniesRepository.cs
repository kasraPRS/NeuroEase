using NeuroEase.Core.Model.Dto;
using NeuroEase.Core.Model.Models;

namespace NeuroEase.Application.Interface
{
    public interface ICompaniesRepository
    {
        Task<CompaniesResultModel> createCompaniesAsync(CompanyDataDto dto);
    }
}
