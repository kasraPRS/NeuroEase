using NeuroEase.Application.Authentication.Dto;
using NeuroEase.Core.Model.Models;


namespace Application.Layer.Interface
{
    public interface IAuthenticationRepository
    {
        Task<AuthResultModel> RegisterAsync(UserLoginRequestDto dto);
        Task<AuthResultModel> LoginAsync(UserLoginRequestDto dto);
    }
}
