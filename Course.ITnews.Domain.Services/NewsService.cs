using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var post in result)
            {
                User user = unitOfWork.Get<User>(post.AuthorId);
                post.Author = user.UserName;
                var ratings = unitOfWork.FindByCondition<Rating>(x => x.NewsId == post.Id);
                post.Ratings = new List<RatingViewModel>();
                foreach (var rating in ratings)
                {
                    post.Ratings.Add(new RatingViewModel()
                    {
                        Id = rating.Id,
                        NewsId = rating.NewsId.GetValueOrDefault(),
                        UserId = rating.AuthorId.GetValueOrDefault(),
                        RatingNumber = rating.RatingNumber
                    });
                }
                var allRating = 0.0;
                foreach (var rating in post.Ratings)
                {
                    allRating += rating.RatingNumber;
                }
                post.AverageRating = allRating / post.Ratings.Count;
                post.TagsIds = new List<int>();

            }
            return result;
        }
        public NewsViewModel Get(int id)
        {
            News news = unitOfWork.Get<News>(id);
            Category category = unitOfWork.Get<Category>(news.CategoryId.GetValueOrDefault());
            news.Category = category;
            User user = unitOfWork.Get<User>(news.AuthorId.GetValueOrDefault());
            news.Author = user;
            var result = mapper.Map<NewsViewModel>(news);
            var tags = unitOfWork.FindByCondition<NewsTag>(x => x.NewsId == news.Id);
            var ratings = unitOfWork.FindByCondition<Rating>(x => x.NewsId == news.Id);

            result.Commentaries = new List<CommentaryViewModel>();
            result.Commentaries = GetCommentaries(result);
            result.Ratings = new List<RatingViewModel>();
            foreach (var rating in ratings)
            {
                result.Ratings.Add(new RatingViewModel()
                {
                    Id = rating.Id,
                    NewsId = rating.NewsId.GetValueOrDefault(),
                    UserId = rating.AuthorId.GetValueOrDefault(),
                    RatingNumber = rating.RatingNumber
                });
            }
            var allRating = 0.0;
            foreach (var rating in result.Ratings)
            {
                allRating += rating.RatingNumber;
            }

            result.AverageRating = allRating / result.Ratings.Count;
            result.TagsIds = new List<int>();
            foreach (var tag in tags)
            {
                result.TagsIds.Add(tag.TagId);
            }
            return result;

        }
        public void Add(NewsViewModel viewModel)
        {
            viewModel = Tags(viewModel);
            var result = mapper.Map<News>(viewModel);
            var category = unitOfWork.Get<Category>(result.CategoryId.GetValueOrDefault());
            result.Category = category;
            var author = unitOfWork.Get<User>(result.AuthorId.GetValueOrDefault());
            result.Author = author;
            unitOfWork.Add(result);
            result.NewsTags = new List<NewsTag>();
            if (viewModel.TagsIds != null)
            {
                foreach (int tagId in viewModel.TagsIds)
                {
                    var newsTag = new NewsTag() { NewsId = result.Id, TagId = tagId };
                    result.NewsTags.Add(newsTag);

                    unitOfWork.Add<NewsTag>(newsTag);
                }
            }
            unitOfWork.SaveChanges();
        }

        public void Edit(NewsViewModel viewModel)
        {
            viewModel = Tags(viewModel);
            News news = unitOfWork.Get<News>(viewModel.Id);

            mapper.Map(viewModel, news);
            news.Category = unitOfWork.Get<Category>(viewModel.CategoryId);
            news.Author = unitOfWork.Get<User>(viewModel.AuthorId);
            var tags = unitOfWork.GetAll<Tag>();
            List<NewsTag> newsTags = (List<NewsTag>)unitOfWork.FindByCondition<NewsTag>(x => x.NewsId == news.Id);
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
                            unitOfWork.Add<NewsTag>(new NewsTag() { NewsId = news.Id, TagId = tag.Id });
                        }
                    }
                    else
                    {
                        if (newsTags.Exists(x => x.TagId == tag.Id))
                        {
                            unitOfWork.Remove<NewsTag>(newsTags.Find(x => x.TagId == tag.Id).Id);
                        }
                    }
                }
            }
            unitOfWork.Update(news);
            unitOfWork.SaveChanges();
        }
        public void Delete(int id)
        {
            News news = unitOfWork.Get<News>(id);
            unitOfWork.Remove<News>(id);
            unitOfWork.SaveChanges();
        }
        public List<SelectListItem> GetCategories()
        {
            var categories = new List<SelectListItem>();
            foreach (var category in unitOfWork.GetAll<Category>())
            {
                categories.Add(new SelectListItem() { Value = category.Id.ToString(), Text = category.Title });
            }
            return categories;
        }
        public NewsViewModel Tags(NewsViewModel viewModel)
        {
            var tempList = unitOfWork.GetAll<Tag>();
            viewModel.TagsIds = new List<int>();
            foreach (var title in viewModel.TagsTitles)
            {
                var tag = tempList.FirstOrDefault(x => x.Title == title);
                if (tag != null)
                {
                    viewModel.TagsIds.Add(tag.Id);
                }
                else
                {
                    Tag tempTag = new Tag() { Title = title };
                    unitOfWork.Add<Tag>(tempTag);
                    var newTag = unitOfWork.FindByCondition<Tag>(x => x.Title == tempTag.Title);
                    foreach (var newId in newTag)
                    {
                        viewModel.TagsIds.Add(newId.Id);
                    }
                }
            }
            return viewModel;
        }

        public List<SelectListItem> GetTags()
        {
            var tags = new List<SelectListItem>();
            foreach (var tag in unitOfWork.GetAll<Tag>())
            {
                tags.Add(new SelectListItem() { Value = tag.Title, Text = tag.Title });
            }
            return tags;
        }

        public List<CommentaryViewModel> GetCommentaries(NewsViewModel viewModel)
        {
            var commentaries = new List<CommentaryViewModel>();
            foreach (var commentary in unitOfWork.GetAll<Commentary>())
            {
                var tempLikes = unitOfWork.FindByCondition<Like>(x => x.CommentId == commentary.Id);
                var resultLikes = mapper.Map<List<LikeViewModel>>(tempLikes);
                if (commentary.NewsId == viewModel.Id)
                    commentaries.Add(new CommentaryViewModel()
                    {
                        AuthorName = commentary.Author.UserName,
                        AuthorId = commentary.AuthorId.GetValueOrDefault(),
                        Created = commentary.Created,
                        Description = commentary.Description,
                        Id = commentary.Id,
                        NewsId = commentary.NewsId.GetValueOrDefault(),
                        Likes = resultLikes
                    });
            }

            return commentaries;
        }

        public NewsViewModel GetTagsTitles(NewsViewModel viewModel)
        {
            var tags = unitOfWork.GetAll<Tag>();
            viewModel.TagsTitles = new List<string>();
            foreach (var tagId in viewModel.TagsIds)
            {
                var tag = tags.FirstOrDefault(x => x.Id == tagId);
                if (tag != null)
                {
                    viewModel.TagsTitles.Add(tag.Title);
                }
            }

            return viewModel;
        }
    }
}
