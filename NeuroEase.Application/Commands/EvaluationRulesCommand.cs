using MediatR;
using NeuroEase.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroEase.Application.Commands
{
    public class EvaluationRulesCommand: IRequest<List<string>>
    {
        public List<UserAnswer> Answers { get; set; }
    }

    public class DiagnosisResult
    {
        public string DiagnosisType { get; set; }
        public string DetailedResult {  get; set; }
    }
}
