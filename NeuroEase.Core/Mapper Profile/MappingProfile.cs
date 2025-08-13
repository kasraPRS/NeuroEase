using AutoMapper;
using NeuroEase.Core.Model.Entity; // محل ApplicationUser
using NeuroEase.Core.Model.Models; // محل AuthResultModel

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, AuthResultModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Token, opt => opt.Ignore()) // Token جدا تولید می‌شود
            .ForMember(dest => dest.Message, opt => opt.Ignore())
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Result, opt => opt.Ignore());
    }
}
