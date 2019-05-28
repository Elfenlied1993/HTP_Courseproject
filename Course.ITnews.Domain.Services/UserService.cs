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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public UserViewModel Get(string id)
        {
            User user = unitOfWork.Get<User>(id);
            var result = mapper.Map<UserViewModel>(user);
            return result;
        }

        public void Add(UserViewModel viewModel)
        {
            var result = mapper.Map<User>(viewModel);
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Edit(UserViewModel viewModel)
        {
            User user = unitOfWork.Get<User>(viewModel.Id);
            mapper.Map(viewModel, user);
            unitOfWork.Update(user);
            unitOfWork.SaveChanges();
        }

        public void Delete(string id)
        {
            User user = unitOfWork.Get<User>(id);
            unitOfWork.Remove<User>(id);
            unitOfWork.SaveChanges();
        }
    }
}
