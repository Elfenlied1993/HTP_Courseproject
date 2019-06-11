using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace Course.ITnews.Web.Hubs
{
    public class RatingHub:Hub
    {
        private readonly INewsService newsService;

        public RatingHub(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public void SendRating(double ratingNumber,int authorId,int newsId)
        {
            var viewModel = newsService.Get(newsId);
        }
    }
}
