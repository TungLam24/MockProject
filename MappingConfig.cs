using AutoMapper;
using MockProject.Models;
using MockProject.Models;
using MockProject.Models.User;

namespace MockProject
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RegistrationRequest, LocalUser>().ReverseMap();
            CreateMap<Product, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
