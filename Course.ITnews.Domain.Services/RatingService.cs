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
    public class RatingService:IRatingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RatingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<RatingViewModel> GetAll()
        {
            IEnumerable<Rating> ratings = unitOfWork.GetAll<Rating>();
            var result = mapper.Map<IEnumerable<RatingViewModel>>(ratings);
            return result;
        }

        public RatingViewModel Get(int id)
        {
            Rating rating = unitOfWork.Get<Rating>(id);
            var result = mapper.Map<RatingViewModel>(rating);
            return result;
        }

        public void Add(RatingViewModel viewModel)
        {
            var result = mapper.Map<Rating>(viewModel);
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Edit(RatingViewModel viewModel)
        {
            Rating rating = unitOfWork.Get<Rating>(viewModel.Id);
            mapper.Map(viewModel, rating);
            unitOfWork.Update(rating);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            unitOfWork.Remove<Rating>(id);
            unitOfWork.SaveChanges();
        }
    }
}
