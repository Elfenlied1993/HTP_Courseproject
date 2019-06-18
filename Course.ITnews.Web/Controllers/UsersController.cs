using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Course.ITnews.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace Course.ITnews.Web.Controllers
{
    //[Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ICommentaryService commentaryService;
        private readonly IRatingService ratingService;
        private readonly ILikeService likeService;
        private readonly INewsService newsService;
        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IRatingService ratingService, ICommentaryService commentaryService, ILikeService likeService, INewsService newsService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.ratingService = ratingService;
            this.commentaryService = commentaryService;
            this.likeService = likeService;
            this.newsService = newsService;
        }


        public IActionResult Index() => View(_userManager.Users.ToList());

        public async Task<IActionResult> EditRole(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, List<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }

            return NotFound();
        }



        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            var deletedNews = new List<NewsViewModel>();
            var news = newsService.GetAll();
            foreach (var authorNews in news)
            {
                if (authorNews.AuthorId == Convert.ToInt32(id))
                {
                    newsService.Delete(authorNews.Id);
                    foreach (var rating in authorNews.Ratings)
                    {
                        ratingService.Delete(rating.Id);
                    }

                    foreach (var comment in authorNews.Commentaries)
                    {
                        foreach (var like in comment.Likes)
                        {
                            likeService.Delete(like.Id);
                        }
                        commentaryService.Delete(comment.Id);
                    }

                }
            }

            var ratings = ratingService.GetAll();
            foreach (var rating in ratings)
            {
                if (rating.UserId == Convert.ToInt32(id))
                {
                    ratingService.Delete(rating.Id);
                }
            }

            var commentaries = commentaryService.GetAll();
            foreach (var comment in commentaries)
            {

                if (comment.AuthorId == Convert.ToInt32(id))
                {
                    var likes = likeService.GetAll(comment.Id);
                    if (likes != null)
                    {
                        foreach (var like in likes)
                        {
                            likeService.Delete(like.Id);
                        }
                    }

                    commentaryService.Delete(comment.Id);
                }
                else
                {
                    var likes = likeService.GetAll(comment.Id);
                    foreach (var like in likes)
                    {
                        if (like.AuthorId == Convert.ToInt32(id))
                        {
                            likeService.Delete(like.Id);
                        }
                    }
                }
        }
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}