using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Course.ITnews.Web.Models;
using Course.ITnews.Web.Services;
using Markdig;
using Markdig.Helpers;
using Markdig.Syntax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Course.ITnews.Web.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly INewsService newsService;
        private readonly ICommentaryService commentaryService;
        private readonly IRatingService ratingService;
        private readonly ILikeService likeService;
        public NewsController(INewsService newsService, UserManager<User> userManager, ICommentaryService commentaryService, IRatingService ratingService, ILikeService likeService)
        {
            this.newsService = newsService;
            this.userManager = userManager;
            this.commentaryService = commentaryService;
            this.ratingService = ratingService;
            this.likeService = likeService;
        }
        [HttpPost]
        [Authorize]
        public IActionResult DeleteComment(int id)
        {
            var likes = likeService.GetAll(id);
            foreach (var like in likes)
            {
                if (like.CommentId == id)
                    likeService.Delete(like.Id);
            }
            
            commentaryService.Delete(id);
            return NoContent();
        }
        [Authorize]
        [HttpPost]
        public async Task<string> ChangeProfile(string id,string value,string username)
        {
            var currentUser = userManager.Users.FirstOrDefault(x => x.UserName == username);
            if (id == "DateOfBirth")
            {
                DateTime date = Convert.ToDateTime(value);
                typeof(User).GetProperty(id).SetValue(currentUser, date);
                await userManager.UpdateAsync(currentUser);
                return value;
            }
            typeof(User).GetProperty(id).SetValue(currentUser, value);
            await userManager.UpdateAsync(currentUser);
            return value;
        }
        public IActionResult Index()
        {
            return View(newsService.GetAll().ToList());
        }

        public async Task<IActionResult> UsersNews(string username,string sortOrder,string searchString,int? pageNumber,string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "cat_desc" : "Category";
            ViewData["Username"] = username;
            ViewData["CurrentFilter"] = searchString;
            var allNews = newsService.GetAll();
            var result = new List<NewsViewModel>();
            
            foreach (var news in allNews)
            {
                if (news.Author == username)
                {
                    result.Add(news);
                }
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Title.Contains(searchString) || s.ShortDescription.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "title_desc":
                    result = result.OrderByDescending(s => s.Title).ToList();
                    break;
                case "Date":
                    result = result.OrderBy(s => s.Created).ToList();
                    break;
                case "date_desc":
                    result= result.OrderByDescending(s => s.Created).ToList();
                    break;
                case "Category":
                    result = result.OrderBy(s => s.Category).ToList();
                    break;
                case "cat_desc":
                    result = result.OrderByDescending(s => s.Category).ToList();
                    break;
                default:
                    result = result.OrderBy(s => s.Title).ToList();
                    break;
            }
            int pageSize = 1;
            return View(await PaginatedList<NewsViewModel>.CreateAsync(result.AsQueryable().Cast<NewsViewModel>(),pageNumber??1,pageSize));
        }
        //GET News/Edit/1
        [Authorize(Roles = "admin,writer")]
        public IActionResult Edit(int id)
        {
            NewsViewModel viewModel = newsService.Get(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            PopPopulateLists(viewModel);
            return View(viewModel);
        }
        [Authorize]
        //POST News/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NewsViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                newsService.Edit(viewModel);
                return RedirectToAction("Index");
            }
            PopPopulateLists(viewModel);
            return View(viewModel);
        }

        //GET News/Create
        [Authorize(Roles = "admin,writer")]
        public IActionResult Create()
        {
            var viewModel = new NewsViewModel();
            PopPopulateLists(viewModel);
            return View(viewModel);
        }
        [Authorize]
        //POST News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, NewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                viewModel.AuthorId = currentUser.Id;
                newsService.Add(viewModel);
                return RedirectToAction("Index");
            }
            PopPopulateLists(viewModel);
            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            NewsViewModel viewModel = newsService.Get(id);
            var currentUser = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (currentUser != null)
            {
                viewModel.NewComment = new CommentaryViewModel();
                viewModel.NewComment.AuthorName = currentUser.UserName;
                viewModel.NewComment.AuthorId = currentUser.Id;
            }
            viewModel.Commentaries = newsService.GetCommentaries(viewModel);
            if (viewModel.Commentaries.Count == 0)
            {
                viewModel.Commentaries.Add(new CommentaryViewModel()
                {
                    Id = 1,
                });
            }
            ViewData["LastId"] = viewModel.Commentaries.LastOrDefault().Id;
            viewModel = newsService.GetTagsTitles(viewModel);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(NewsViewModel viewModel)
        {
            return RedirectToAction("Details");
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var viewModel = newsService.Get(id);
            foreach (var rating in viewModel.Ratings)
            {
                ratingService.Delete(rating.Id);
            }

            foreach (var comment in viewModel.Commentaries)
            {
                foreach (var like in comment.Likes)
                {
                    likeService.Delete(like.Id);
                }
                commentaryService.Delete(comment.Id);
            }
            newsService.Delete(id);
            return RedirectToAction("Index");
        }
        public void PopPopulateLists(NewsViewModel viewModel)
        {
            viewModel.Categories = newsService.GetCategories();
            viewModel.Tags = newsService.GetTags();
        }
    }
}