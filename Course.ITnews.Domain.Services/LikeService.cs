using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<LikeViewModel> GetAll()
        {
            IEnumerable<Like> ratings = unitOfWork.GetAll<Like>();
            var result = mapper.Map<IEnumerable<LikeViewModel>>(ratings);
            return result;
        }

        public LikeViewModel Get(int id)
        {
            Like rating = unitOfWork.Get<Like>(id);
            var result = mapper.Map<LikeViewModel>(rating);
            return result;
        }

        public void Add(LikeViewModel viewModel)
        {
            var result = mapper.Map<Like>(viewModel);
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            Like like = unitOfWork.Get<Like>(id);
            unitOfWork.Remove<Like>(id);
            unitOfWork.SaveChanges();
        }
    }
}
