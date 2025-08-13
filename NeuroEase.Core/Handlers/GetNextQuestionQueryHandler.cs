using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Layer.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NeuroEase.Application.Queries;
using NeuroEase.Core.Data;
namespace NeuroEase.Core.Helpers
{
    public class GetNextQuestionQueryHandler : IRequestHandler<GetNextQuestionQuery, Question>
    {
        private readonly NeuroEaseDbContext _context;

        public GetNextQuestionQueryHandler(NeuroEaseDbContext context)
        {
            _context = context;
        }
        public async Task<Question> Handle(GetNextQuestionQuery nextQuestionQuery, CancellationToken cancellationToken)
        {
            var answerdQuestuinIds = await _context.UserAnswers
                .Where(user => user.UserId == nextQuestionQuery.UserId)
                .Select(user => user.QuestionId)
                .ToListAsync(cancellationToken);

            var nextQuestion = await _context.Questions
                .Where(question => !answerdQuestuinIds.Contains(question.Id))
                .OrderBy(question => question.Order)
                .FirstOrDefaultAsync(cancellationToken);

            return nextQuestion; 

        }
    }
}
