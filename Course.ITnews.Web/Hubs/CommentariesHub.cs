using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Course.ITnews.Web.Hubs
{
    public class CommentariesHub : Hub
    {
        private readonly ICommentaryService commentaryService;

        public CommentariesHub(ICommentaryService commentaryService)
        {
            this.commentaryService = commentaryService;
        }

        public void SendCommentary(string user, string message, int authorId, int newsId)
        {
            CommentaryViewModel viewModel = new CommentaryViewModel();
            viewModel.AuthorId = authorId;
            viewModel.Description = message;
            viewModel.Created = DateTime.Now;
            viewModel.NewsId = newsId;
            commentaryService.Add(viewModel);
            Clients.All.SendAsync("ReceiveCommentary", user, message, authorId, newsId);
        }

    }
}
