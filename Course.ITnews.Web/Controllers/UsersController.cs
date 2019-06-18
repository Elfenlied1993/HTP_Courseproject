using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Course.ITnews.Domain.Services;
using Course.ITnews.Web.Hubs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Rewrite.Internal.IISUrlRewrite;
using Microsoft.CodeAnalysis.CSharp;

namespace Course.ITnews.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly ICommentaryService commentaryService;
        private readonly IRatingService ratingService;
        private readonly ILikeService likeService;
        private readonly INewsService newsService;
        private readonly SignInManager<User> _signInManager;
        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IRatingService ratingService, ICommentaryService commentaryService, ILikeService likeService, INewsService newsService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.ratingService = ratingService;
            this.commentaryService = commentaryService;
            this.likeService = likeService;
            this.newsService = newsService;
            _signInManager = signInManager;
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
        [AllowAnonymous]
        public async Task<IActionResult> Details(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            
            return View(user);
        }

      
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var genders = new List<string>
            {
                "Male",
                "Female"
            };

            ViewData["Genders"] = new SelectList(genders);
            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            string file = null;
            if (user.UserPhoto != null)
            {
                file = Convert.ToBase64String(user.UserPhoto);
            }

            Username = userName;

            Input = new InputModel
            {
                Name = user.Name,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Specialization = user.Specialization,
                Photo = file,
                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return View(Input);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(InputModel Input)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            user.Name = Input.Name;
            user.Country = Input.Country;
            user.Gender = Input.Gender;
            user.Specialization = Input.Specialization;
            user.DateOfBirth = Input.DateOfBirth;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToAction("Index","News");
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