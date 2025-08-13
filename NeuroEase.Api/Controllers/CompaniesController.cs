using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeuroEase.Application.Commands;
using NeuroEase.Core.Data;

namespace NeuroEase.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly NeuroEaseDbContext _dbcontext;

        public CompaniesController(IMediator mediator, NeuroEaseDbContext dbcontext)
        {
            _mediator = mediator;
            _dbcontext = dbcontext;
        }

        [HttpPost("company")]
        public async Task<IActionResult> CreateCompany([FromBody] CompaniesSetDataCommand companyData)
        {
            if (companyData == null)
                return BadRequest("");
            try
            {
                var restlt = await _mediator.Send(companyData);
                return Ok(companyData);
            }
            catch (Exception ex) { }
            return Unauthorized();
        }
    }
}
