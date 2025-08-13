using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroEase.Core.Model.Models;
namespace NeuroEase.Application.Authentication.Command
{
    public class LoginCommand : IRequest<AuthResultModel>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
