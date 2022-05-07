using AutoMapper;
using PortfolioProApi.Models;
using PortfolioProApi.Entities;

namespace PortfolioProApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email)).ReverseMap();

            CreateMap<User, AuthResponseDto>().ReverseMap();

            CreateMap<Transaction, TransactionDto>()
                .ForMember(u=>u.UserId, opt=>opt.MapFrom(ui=>ui.UserId)).ReverseMap();

            CreateMap<Transaction, TransactionPutDto>().ReverseMap();
        }
    }
}
