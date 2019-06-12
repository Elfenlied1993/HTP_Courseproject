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

        public void SendCommentary(string user, string message, int authorId, int newsId, int commentId)
        {
            CommentaryViewModel viewModel = new CommentaryViewModel();
            var comments = commentaryService.GetAll();
            var existingComment = comments.FirstOrDefault(x => x.Id == commentId);
            if (existingComment != null)
            {
                commentId++;
            }

            viewModel.AuthorName = user;
            viewModel.Id = commentId;
            viewModel.AuthorId = authorId;
            viewModel.Description = message;
            viewModel.Created = DateTime.Now;
            viewModel.NewsId = newsId;
            commentaryService.Add(viewModel);
            Clients.All.SendAsync("ReceiveCommentary", viewModel.AuthorName, viewModel.Description, viewModel.AuthorId,
                viewModel.NewsId, viewModel.Id);
        }

        public void DeleteCommentary(int commentId)
        {
            Clients.All.SendAsync("ReceiveDelete", commentId);
        }

    }
}