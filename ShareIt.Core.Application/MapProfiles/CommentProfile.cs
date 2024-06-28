using AutoMapper;
using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.MapProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile() {
            CreateMap<CommentViewModel, Comment>()
            .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
            .ForMember(dest => dest.Replies, opt => opt.Ignore())
            .ForMember(dest => dest.publication, opt => opt.Ignore())
            .ReverseMap();

            // Map from CommentSaveViewModel to Comment
            CreateMap<CommentSaveViewModel, Comment>()
                .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore())
                .ForMember(dest => dest.publication, opt => opt.Ignore())
                .ReverseMap();

            // Map from Comment to CommentViewModel
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
                .ReverseMap();
        }
    }
}
