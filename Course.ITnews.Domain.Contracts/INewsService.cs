using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;
using Course.ITnews.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course.ITnews.Domain.Contracts
{
    public interface INewsService
    {
        IEnumerable<NewsViewModel> GetAll();
        NewsViewModel Get(int id);
        void Add(NewsViewModel viewModel);
        void Edit(NewsViewModel viewModel);
        List<TagModel> TagsCloud();
        void Delete(int id);
        List<SelectListItem> GetCategories();
        NewsViewModel GetTagsTitles(NewsViewModel viewModel);
        List<SelectListItem> GetTags();
        List<CommentaryViewModel> GetCommentaries(NewsViewModel viewModel);
    }
}
