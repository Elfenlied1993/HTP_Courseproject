using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Infrastructure.MappingProfiles
{
    public class RatingMappingProfile :Profile 
    {
        public RatingMappingProfile()
        {
            MapRatingToRatingViewModel();
            MapRatingViewModelToRating();
        }

        private void MapRatingToRatingViewModel()
        {
            CreateMap<Rating,RatingViewModel>()
                .ForMember(dest=>dest.Id,c=>c.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.NewsId,c=>c.MapFrom(src=>src.NewsId))
                .ForMember(dest=>dest.UserId,c=>c.MapFrom(src=>src.AuthorId))
                .ForMember(dest=>dest.RatingNumber,c=>c.MapFrom(src=>src.RatingNumber))
                .ForAllOtherMembers(c=>c.Ignore());
        }

        private void MapRatingViewModelToRating()
        {
            CreateMap<RatingViewModel, Rating>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.NewsId, c => c.MapFrom(src => src.NewsId))
                .ForMember(dest => dest.AuthorId, c => c.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RatingNumber, c => c.MapFrom(src => src.RatingNumber))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
