using Core.Model.Layer.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeuroEase.Application.Commands;
using NeuroEase.Application.Queries;
using NeuroEase.Core.Data;
using NeuroEase.Core.Model.Entity;
using System.Security.Claims;

namespace NeuroEase.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly NeuroEaseDbContext _dbcontext;

        public DiagnosisController(IMediator mediator, NeuroEaseDbContext dbContext)
        {
            _mediator = mediator;
            _dbcontext = dbContext;
        }

        [HttpPost("evaluate")]
        public async Task<IActionResult> Evaluate([FromBody] UserAnswer answer)
        {
            if (answer == null)
                return BadRequest("");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("شناسه کاربر در توکن یافت نشد.");
            answer.UserId = userId;

            var sessionId = HttpContext.Session.GetString("DiagnosisSessionId") ?? Guid.NewGuid().ToString();
            answer.SessionId = sessionId;
            HttpContext.Session.SetString("DiafnosisSessionId", sessionId);

            try
            {
                var diagnoses = await _mediator.Send(new EvaluationRulesCommand
                {
                    Answers = new List<UserAnswer> { answer }
                });
                var result = diagnoses.Select(diagnos => new DiagnosisResult
                {
                    DiagnosisType = diagnos,
                    DetailedResult = $"تشخیص: {diagnos}. برای اطلاعات بیشتر با پزشک مشورت کنید."
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiagnosis([FromBody] DiagnosisCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diagnosis = new Diagnosis
            {
                SessionId = dto.SessionId,
                Result = dto.Result,
                DiagnosticRuleId = dto.DiagnosticRuleId,
                Code = dto.Code,
                Title = dto.Title,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            _dbcontext.Diagnoses.Add(diagnosis);
            await _dbcontext.SaveChangesAsync();

            return Ok(diagnosis);
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNextQuestion()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Not user find");

            try
            {
                var nextQuestion = await _mediator.Send(new GetNextQuestionQuery(userId));
                if (nextQuestion == null)
                    return Ok(new { Done = true, Message = "All question has been answerd" });

                return Ok(nextQuestion);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}