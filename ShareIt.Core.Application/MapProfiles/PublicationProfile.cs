using AutoMapper;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.MapProfiles
{
    public class PublicationProfile : Profile
    {

        public PublicationProfile() {
            CreateMap<PublicationSaveViewModel, Publication>()
        .ForMember(dest => dest.Photo, opt => opt.Ignore())
        .ForMember(dest => dest.Profile, opt => opt.Ignore())
        .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ReverseMap()
        .ForMember(dest => dest.Photo, opt => opt.Ignore());


            CreateMap<Publication, PublicationViewModel>()
            .ForMember(dest => dest.Username, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.ToList() : new List<Comment>()))
            .ReverseMap();
        }
    }
}
