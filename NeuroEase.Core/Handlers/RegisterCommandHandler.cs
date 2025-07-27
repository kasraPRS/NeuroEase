
using Application.Layer.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NeuroEase.Application.Authentication.Command;
using NeuroEase.Core.Model.Entity;
using NeuroEase.Core.Model.Models;
namespace NeuroEase.Core.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResultModel>
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(IAuthenticationRepository authenticationRepository,UserManager<ApplicationUser> userManager, ILogger<RegisterCommandHandler> logger)
        {
            _authenticationRepository = authenticationRepository;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<AuthResultModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Processing registration for email: {request.Email}");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User registered successfully: {request.Email}");
                return new AuthResultModel
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Result = true,
                    Success = true,
                    Message = "ثبت‌نام با موفقیت انجام شد"
                };
            }

            _logger.LogError($"Registration failed for {request.Email}: {string.Join("; ", result.Errors.Select(e => e.Description))}");
            return new AuthResultModel
            {
                Success = false,
                Message = string.Join("; ", result.Errors.Select(e => e.Description))
            };
        }
    }
}
