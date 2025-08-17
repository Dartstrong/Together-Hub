using Application.Security.Dtos;
using Application.Topics.Dtos;
using AutoMapper;
using Domain.Security;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UpdateTopicDto, Topic>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                    src.Location.Country,
                    src.Location.City,
                    src.Location.Street
                )))
                .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => dest.Id));

            CreateMap<CreateTopicDto, Topic>()
               .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                   src.Location.Country,
                   src.Location.City,
                   src.Location.Street
               )))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => TopicId.Of(Guid.NewGuid())));

            CreateMap<RegisterUserRequestDto, CustomIdentityUser>()
                .ForMember(dest => dest.About, opt => opt.MapFrom( src => String.Empty));

            CreateMap<UserProfileDto, CustomIdentityUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

            CreateMap<RelationshipDto, Relationship>()
                .ForMember(dest => dest.TopicReference, opt => opt.MapFrom(src => src.TopicReference))
                .ForMember(dest => dest.UserReference, opt => opt.MapFrom(src => src.UserReference))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        }
    }
}
