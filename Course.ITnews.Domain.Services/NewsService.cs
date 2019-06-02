using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IEnumerable<NewsViewModel> GetAll()
        {
            IEnumerable<News> news = unitOfWork.GetAll<News>();
            var result = mapper.Map<IEnumerable<NewsViewModel>>(news);
            return result;
        }


        public NewsViewModel Get(string id)
        {
            News news = unitOfWork.Get<News>(id);
            Category category = unitOfWork.Get<Category>(news.CategoryId);
            news.Category = category;
            User user = unitOfWork.Get<User>(news.AuthorId);
            news.Author = user;
            var result = mapper.Map<NewsViewModel>(news); 
            var tags = unitOfWork.FindByCondition<NewsTag>(x => x.NewsId == news.Id);
            var commentaries = unitOfWork.FindByCondition<Commentary>(x => x.NewsId == news.Id);
            result.TagsIds = new List<string>();
            result.CommentariesIds = new List<string>();
            foreach (var tag in tags)
            {
                result.TagsIds.Add(tag.TagId);
            }
            foreach (var commentary in commentaries)
            {
                result.CommentariesIds.Add(commentary.Id);
            }
            return result;

        }
        public void Add(NewsViewModel viewModel)
        {
            var result = mapper.Map<News>(viewModel);
            var category = unitOfWork.Get<Category>(result.CategoryId);
            result.Category = category;
            var author = unitOfWork.Get<User>(result.AuthorId);
            result.Author = author;
            result.NewsTags = new List<NewsTag>();
            if (viewModel.TagsIds != null)
            {
                foreach (string tagId in viewModel.TagsIds)
                {
                    var newsTag = new NewsTag() {NewsId = result.Id, TagId = tagId};
                    result.NewsTags.Add(newsTag);
                    unitOfWork.Add<NewsTag>(newsTag);
                }
            }
            //result.Commentaries = new List<Commentary>();
            //if (viewModel.CommentariesIds != null)
            //{
            //    foreach (string commentaryId in viewModel.CommentariesIds)
            //    {
            //        var commentary = new Commentary(){NewsId = result.Id,Id = commentaryId};
            //        result.Commentaries.Add(commentary);
            //        unitOfWork.Add<Commentary>(commentary);
            //    }
            //}
            unitOfWork.Add(result);
            unitOfWork.SaveChanges();
        }

        public void Edit(NewsViewModel viewModel)
        {
            News news = unitOfWork.Get<News>(viewModel.Id);
            mapper.Map(viewModel, news);
            news.Category = unitOfWork.Get<Category>(viewModel.CategoryId);
            news.Author = unitOfWork.Get<User>(viewModel.AuthorId);
            var tags = unitOfWork.GetAll<Tag>();
            List<NewsTag> newsTags = (List<NewsTag>) unitOfWork.FindByCondition<NewsTag>(x => x.NewsId == news.Id);
            if (viewModel.TagsIds == null)
            {
                foreach (var newsTag in newsTags)
                {
                    unitOfWork.Remove<NewsTag>(newsTag.Id);
                }
            }
            else
            {
                foreach (var tag in tags)
                {
                    if (viewModel.TagsIds.Contains(tag.Id))
                    {
                        if (newsTags.Exists(x => x.TagId == tag.Id))
                        {
                            unitOfWork.Update<NewsTag>(newsTags.Find(x => x.TagId == tag.Id));
                        }
                        else
                        {
                            unitOfWork.Add<NewsTag>(new NewsTag(){NewsId = news.Id,TagId = tag.Id});
                        }
                    }
                    else
                    {
                        if (newsTags.Exists(x => x.TagId == tag.Id))
                        {
                            unitOfWork.Remove<NewsTag>(newsTags.Find(x=>x.TagId==tag.Id).Id);
                        }
                    }
                }
            }
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
