using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Domain.Contracts
{
    public interface IUserService
    {
        UserViewModel Get(string id);
        void Add(UserViewModel viewModel);
        void Edit(UserViewModel viewModel);
        void Delete(string id);
    }
}
