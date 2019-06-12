using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Course.ITnews.Web.Hubs
{
    public class RatingHub : Hub
    {
        private readonly INewsService newsService;
        private readonly IRatingService ratingService;

        public RatingHub(INewsService newsService, IRatingService ratingService)
        {
            this.newsService = newsService;
            this.ratingService = ratingService;
        }

        public void SendRating(double ratingNumber, int authorId, int newsId)
        {
            var newsViewModel = newsService.Get(newsId);
            var tempRating = newsViewModel.Ratings.FirstOrDefault(x => x.UserId == authorId && x.NewsId==newsId);
            if (tempRating != null)
            {
                ratingService.Edit(new RatingViewModel()
                {
                    NewsId = newsId,
                    RatingNumber = ratingNumber,
                    Id = tempRating.Id,
                    UserId = authorId
                });
            }
            else
            {
                ratingService.Add(new RatingViewModel()
                {
                    NewsId = newsId,
                    RatingNumber = ratingNumber,
                    UserId = authorId
                });
            }
        }
    }
}
