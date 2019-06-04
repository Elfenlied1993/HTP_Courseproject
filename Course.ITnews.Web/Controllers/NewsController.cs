﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Domain.Contracts;
using Course.ITnews.Domain.Contracts.ViewModels;
using Course.ITnews.Web.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace Course.ITnews.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
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

            return View(viewModel);
        }
        //GET News/Create
        public IActionResult Create()
        {
            var viewModel = new NewsViewModel();
            return View(viewModel);
        }
        //POST News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, NewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.FullDescription = Markdown.ToHtml(viewModel.FullDescription);
                newsService.Add(viewModel);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}