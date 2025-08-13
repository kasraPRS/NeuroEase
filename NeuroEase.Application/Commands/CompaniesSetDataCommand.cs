using MediatR;
using NeuroEase.Core.Model.Entity;
using NeuroEase.Core.Model.Models;

namespace NeuroEase.Application.Commands
{
    public class CompaniesSetDataCommand : IRequest<CompaniesResultModel>
    {
        public List<CompanyData> Company { get; set; }
    }
}
