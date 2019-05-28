using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Infrastructure.MappingProfiles
{
    public class CommentaryMappingProfile : Profile
    {
        public CommentaryMappingProfile()
        {
            MapCommentaryToCommentaryViewModel();
            MapCommentaryViewModelToCommentary();
        }

        private void MapCommentaryToCommentaryViewModel()
        {
            CreateMap<Commentary, CommentaryViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.AuthorName, c => c.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.Created, c => c.MapFrom(src => src.Created))
                .ForMember(dest => dest.Description, c => c.MapFrom(src => src.Description))
                .ForMember(dest => dest.NewsId, c => c.MapFrom(src => src.NewsId))
                .ForMember(dest => dest.Title, c => c.MapFrom(src => src.Title))
                .ForAllOtherMembers(c=>c.Ignore());
        }

        private void MapCommentaryViewModelToCommentary()
        {
            CreateMap<CommentaryViewModel,Commentary>()
                .ForMember(dest=>dest.Id,c=>c.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.Title,c=>c.MapFrom(src=>src.Title))
                .ForMember(dest=>dest.NewsId,c=>c.MapFrom(src=>src.NewsId))
                .ForMember(dest=>dest.AuthorId,c=>c.MapFrom(src=>src.AuthorId))
                .ForMember(dest=>dest.Created,c=>c.MapFrom(src=>src.Created))
                .ForMember(dest=>dest.Description,c=>c.MapFrom(src=>src.Description))
                .ForAllOtherMembers(c=>c.Ignore());
        }
    }
}
