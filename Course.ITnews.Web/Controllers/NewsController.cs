﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Course.ITnews.Web.Models;
using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace Course.ITnews.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly INewsService newsService;

        public NewsController(INewsService newsService, UserManager<User> userManager)
        {
            this.newsService = newsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(newsService.GetAll().ToList());
        }
        //GET News/Edit/1
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
        public IActionResult Create()
        {
            var viewModel = new NewsViewModel();
            PopPopulateLists(viewModel);
            return View(viewModel);
        }
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
        public void PopPopulateLists(NewsViewModel viewModel)
        {
            viewModel.Categories = newsService.GetCategories();
            viewModel.Tags = newsService.GetTags();
        }
    }
}