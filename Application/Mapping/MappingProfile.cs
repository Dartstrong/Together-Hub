using AutoMapper;

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
        }
    }
}
