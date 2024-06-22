using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShareIt.Core.Application;
using ShareItApp.Models;
using System.Diagnostics;

namespace ShareItApp.Controllers
{
    public class PublicationController : Controller
    {

        public PublicationController()
        {
           
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(new PublicationIndexViewModel
            {
                UserClaim = User,
                Svm = new PublicationSaveViewModel(),
                Publications = new List<PublicationViewModel>()
            });
        }



    
    }
}
