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


        public CommentaryViewModel Get(string id)
        {
            Commentary commentary = unitOfWork.Get<Commentary>(id);
            var result = mapper.Map<CommentaryViewModel>(commentary);
            return result;
        }

        public void Add(CommentaryViewModel viewModel)
        {
            var result = mapper.Map<Commentary>(viewModel);
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Edit(CommentaryViewModel viewModel)
        {
            Commentary commentary = unitOfWork.Get<Commentary>(viewModel.Id);
            mapper.Map(viewModel, commentary);
            unitOfWork.Update(commentary);
            unitOfWork.SaveChanges();
        }

        public void Delete(string id)
        {
            Commentary commentary = unitOfWork.Get<Commentary>(id);
            unitOfWork.Remove<Commentary>(id);
            unitOfWork.SaveChanges();
        }
    }
}
