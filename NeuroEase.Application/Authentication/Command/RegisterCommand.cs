using MediatR;
using NeuroEase.Core.Model.Models;

namespace NeuroEase.Application.Authentication.Command
{
    public class RegisterCommand : IRequest<AuthResultModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; } = null;
    }
}
