using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Infrastructure.MappingProfiles
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            MapNewsToNewsViewModel();
            MapNewsViewModelToNews();
        }

        private void MapNewsToNewsViewModel()
        {
            CreateMap<News,NewsViewModel>()
                .ForMember(dest=>dest.Id,c=>c.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.Author,c=>c.MapFrom(src=>src.Author.UserName))
                .ForMember(dest=>dest.AuthorId,c=>c.MapFrom(src=>src.AuthorId))
                .ForMember(dest=>dest.Category,c=>c.MapFrom(src=>src.Category.Title))
                .ForMember(dest=>dest.CategoryId,c=>c.MapFrom(src=>src.CategoryId))
                .ForMember(dest=>dest.FullDescription,c=>c.MapFrom(src=>src.FullDescription))
                .ForMember(dest=>dest.ShortDescription,c=>c.MapFrom(src=>src.ShortDescription))
                .ForMember(dest=>dest.Created,c=>c.MapFrom(src=>src.Created))
                .ForMember(dest=>dest.Title,c=>c.MapFrom(src=>src.Title))
                .ForAllOtherMembers(c=>c.Ignore());
        }

        private void MapNewsViewModelToNews()
        {
            CreateMap<NewsViewModel, News>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, c => c.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.CategoryId, c => c.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.FullDescription, c => c.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.ShortDescription, c => c.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.Title, c => c.MapFrom(src => src.Title))
                .ForMember(dest => dest.Created, c => c.MapFrom(src => src.Created))
                .ForAllOtherMembers(c=>c.Ignore());
        }
    }
}
