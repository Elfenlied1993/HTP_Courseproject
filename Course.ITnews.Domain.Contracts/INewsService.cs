using System;
using System.Collections.Generic;
using System.Text;
using Course.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course.ITnews.Domain.Contracts
{
    public interface INewsService
    {
        IEnumerable<NewsViewModel> GetAll();
        NewsViewModel Get(string id);
        void Add(NewsViewModel viewModel);
        void Edit(NewsViewModel viewModel);
        void Delete(string id);
    }
}
