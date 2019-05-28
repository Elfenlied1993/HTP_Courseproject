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
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public NewsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public NewsViewModel Get(string id)
        {
            News news = unitOfWork.Get<News>(id);
            var result = mapper.Map<NewsViewModel>(news);
            return result;

        }

        public void Add(NewsViewModel viewModel)
        {
            var result = mapper.Map<News>(viewModel);
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Edit(NewsViewModel viewModel)
        {
            News news = unitOfWork.Get<News>(viewModel.Id);
            mapper.Map(viewModel, news);
            unitOfWork.Update(news);
            unitOfWork.SaveChanges();
        }

        public void Delete(string id)
        {
            News news = unitOfWork.Get<News>(id);
            unitOfWork.Remove<News>(id);
            unitOfWork.SaveChanges();
        }
    }
}
