using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.ITnews.Web.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}