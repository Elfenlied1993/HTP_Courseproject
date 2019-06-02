using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Services
{
    public class CommentaryService : ICommentaryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentaryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        public IEnumerable<CommentaryViewModel> GetAll()
        {
            IEnumerable<Commentary> commentaries = unitOfWork.GetAll<Commentary>();
            var result = mapper.Map<IEnumerable<CommentaryViewModel>>(commentaries);
            return result;
        }

        public CommentaryViewModel Get(int id)
        {
            Commentary commentary = unitOfWork.Get<Commentary>(id);
            var news = unitOfWork.Get<News>(commentary.NewsId.GetValueOrDefault());
            commentary.News = news;
            var author = unitOfWork.Get<User>(commentary.AuthorId.GetValueOrDefault());
            commentary.Author = author;
            var result = mapper.Map<CommentaryViewModel>(commentary);
            return result;
        }

        public void Add(CommentaryViewModel viewModel)
        {
            var result = mapper.Map<Commentary>(viewModel);
            var news = unitOfWork.Get<News>(result.NewsId.GetValueOrDefault());
            result.News = news;
            var author = unitOfWork.Get<User>(result.AuthorId.GetValueOrDefault());
            result.Author = author;
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Edit(CommentaryViewModel viewModel)
        {
            Commentary commentary = unitOfWork.Get<Commentary>(viewModel.Id);
            commentary.Author = unitOfWork.Get<User>(viewModel.AuthorId);
            commentary.News = unitOfWork.Get<News>(viewModel.NewsId);
            mapper.Map(viewModel, commentary);
            unitOfWork.Update(commentary);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            Commentary commentary = unitOfWork.Get<Commentary>(id);
            unitOfWork.Remove<Commentary>(id);
            unitOfWork.SaveChanges();
        }
    }
}
