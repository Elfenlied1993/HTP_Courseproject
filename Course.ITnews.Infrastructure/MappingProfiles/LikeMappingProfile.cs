using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Infrastructure.MappingProfiles
{
    public class LikeMappingProfile : Profile
    {
        public LikeMappingProfile()
        {
            MapLikeToLikeViewModel();
            MapLikeViewModelToLike();
        }

        private void MapLikeToLikeViewModel()
        {
            CreateMap<Like, LikeViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.CommentId, c => c.MapFrom(src => src.CommentId))
                .ForAllOtherMembers(c=>c.Ignore());
        }

        private void MapLikeViewModelToLike()
        {

            CreateMap<LikeViewModel, Like>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.CommentId, c => c.MapFrom(src => src.CommentId))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
