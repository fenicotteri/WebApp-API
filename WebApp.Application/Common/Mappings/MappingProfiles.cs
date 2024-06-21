
using AutoMapper;
using WebApp.Application.Gatherings.Queries.GetAll;
using WebApp.Application.Members.Queries.GetAll;
using WebApp.Application.Members.Queries.GetById;
using WebApp.Domain;
using WebApp.Domain.Entities;
using WebApp.Domain.QueryObjects;

namespace WebApp.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Member, MemberResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Value))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Value))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOnUtc.Date.ToShortDateString()));

        CreateMap<Gathering, GatheringResponse>();

        CreateMap<List<Member>, AllMembersResponse>()
            .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Count));

        CreateMap<List<Gathering>, AllGatheringsResponse>()
            .ForMember(dest => dest.Gatherings, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Count));

        CreateMap<MemberQueryObjectRequest, MemberQueryObject>();
        CreateMap<GatheringQueryObjectRequest, GatheringQueryObject>();

    }
}
