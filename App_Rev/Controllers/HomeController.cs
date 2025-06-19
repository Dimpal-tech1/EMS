using System.Diagnostics;
using App_Rev.Core;
using App_Rev.Infrastructure.Interface;
using App_Rev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App_Rev.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUser _user;

        public HomeController(IUser user)
        {
            _user = user;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _user.GetAll();
            return View(user);
        }
        public async Task<IActionResult> CreateOrEdit(int Id = 0)
        {
            ViewBag.RoleList = new List<SelectListItem>
            {
                new SelectListItem{Text="Admin",Value="Admin"},
                new SelectListItem{Text="Employee",Value="Employee"},
                new SelectListItem{Text="Manager",Value="Manager"},
            };
            if (Id == 0)
            {
                return View(new User());
            }
            else
            {
                try
                {
                    var user = _user.GetById(Id);
                    if (user != null)
                    {
                        return View(user);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = ex.Message;
                }
                TempData["Msg"] = $"User details not found with Id : {Id}";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _user.AddOrUpdate(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Msg"] = "Model state is invalid.";
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["Msg"] = ex.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
