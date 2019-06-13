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
        private readonly ILikeService likeService;

        public CommentariesHub(ICommentaryService commentaryService, ILikeService likeService)
        {
            this.commentaryService = commentaryService;
            this.likeService = likeService;
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

        public void Like(int authorId, int commentId)
        {
            var existingLikes = likeService.GetAll(commentId);
            LikeViewModel viewModel = new LikeViewModel();
            viewModel.AuthorId = authorId;
            viewModel.CommentId = commentId;
            var existingLike = existingLikes.FirstOrDefault(x => x.AuthorId == authorId);
            if (existingLike == null)
            {
                likeService.Add(viewModel);
                var likes = likeService.GetAll(commentId);
                Clients.All.SendAsync("ReceiveLike", viewModel.CommentId, likes.Count());
            }
            else
            {
                likeService.Delete(existingLike.Id);
                var likes = likeService.GetAll(commentId);
                Clients.All.SendAsync("ReceiveLike", viewModel.CommentId, likes.Count());
            }
        }
    }
}