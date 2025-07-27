using AutoMapper;
using NeuroEase.Application.Authentication.Dto;
using NeuroEase.Core.Model.Models;

namespace NeuroEase.Core.Helpers
{
    public class MappingProfile : Profile // ✅ اینجا باید Profile باشه
    {
        public MappingProfile()
        {
            CreateMap<AuthResultModel, AuthResultDto>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "LOGIN_WAS_SUCCESS"))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token));
        }
    }
}
