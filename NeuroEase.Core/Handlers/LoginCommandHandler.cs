using Application.Layer.Interface;
using AutoMapper;
using Core.Layer.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NeuroEase.Application.Authentication.Command;
using NeuroEase.Core.Model.Entity;
using NeuroEase.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroEase.Core.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultModel>
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtHelper _jwtHelper;
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(
            IAuthenticationRepository authRepository,
            SignInManager<ApplicationUser> signInManager,
            IJwtHelper jwtHelper,
            ILogger<LoginCommandHandler> logger,
            UserManager<ApplicationUser> userManager
            )
        {
            _authRepository = authRepository;
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<AuthResultModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Processing login for email: {request.Email}");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning($"User not found: {request.Email}");
                return new AuthResultModel
                {
                    Success = false,
                    Message = "کاربر یافت نشد"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {


                var token = _jwtHelper.GenerateJwtToken(user);
                _logger.LogInformation($"User logged in successfully: {request.Email}");

                return new AuthResultModel
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Result = true,
                    Success = true,
                    Message = "ورود موفقیت‌آمیز بود",
                    Token = token,
                    UserId = user.Id
                };
            }

            _logger.LogError($"Login failed for {request.Email}: Incorrect password");
            return new AuthResultModel
            {
                Success = false,
                Message = "رمز عبور اشتباه است"
            };
        }
    }
}
