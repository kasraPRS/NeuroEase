using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NeuroEase.Core.Model.Entity;
using NeuroEase.Core.Model.Models;
using Core.Layer.Helpers;
using Application.Layer.Interface;
using NeuroEase.Application.Authentication.Dto;
namespace NeuroEase.Core.Repository
{

        public class AuthRepository : IAuthenticationRepository
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IJwtHelper _jwtHelper;
            public ILogger<AuthRepository> Register { get; }

            public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtHelper jwtHelper, ILogger<AuthRepository> repository)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtHelper = jwtHelper;
                Register = repository;
            }

            public async Task<AuthResultModel> RegisterAsync(UserLoginRequestDto dto)
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    return new AuthResultModel
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Result = true,
                        Success = true,
                        Message = "ثبت‌نام با موفقیت انجام شد",
                    };
                }

                return new AuthResultModel
                {
                    Success = false,
                    Message = string.Join("; ", result.Errors.Select(e => e.Description))
                };
            }

            public async Task<AuthResultModel> LoginAsync(UserLoginRequestDto dto)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    return new AuthResultModel
                    {
                        Success = false,
                        Message = "کاربر یافت نشد"
                    };
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

                if (result.Succeeded)
                {
                    var token = _jwtHelper.GenerateJwtToken(user); // تولید توکن با JwtHelper
                    return new AuthResultModel
                    {
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Result = true,
                        Success = true,
                        Message = "ورود موفقیت‌آمیز بود",
                        Token = token,
                    };
                }

                return new AuthResultModel
                {
                    Success = false,
                    Message = "رمز عبور اشتباه است"
                };
            }
        }
    }
