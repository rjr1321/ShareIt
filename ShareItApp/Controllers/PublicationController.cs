using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using ShareItApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ShareItApp.Controllers
{
    public class PublicationController : Controller
    {
        public readonly IPublicationServices _publicationServices;

        public readonly List<PublicationViewModel> Publications;

        public readonly ICommentServices _commentServices;



        public PublicationController(IPublicationServices publicationServices, ICommentServices commentServices)
        {
            _publicationServices = publicationServices;
            Publications = _publicationServices.GetAllViewModel().Result;
            _commentServices = commentServices;
        }

        public async Task<IActionResult> Index(PublicationIndexViewModel vm)
        {
            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            else if (!ModelState.IsValid)
            {
                return View("Index", vm);

            }

            List<PublicationViewModel> list = await _publicationServices.GetAllViewModel();

            string Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(new PublicationIndexViewModel
            {
                UserClaim = User,
                Svm = new PublicationSaveViewModel
                {
                    IdProfile = Id
                },
                Publications = Publications.FindAll(x => x.Profile.IdUser == Id)
            }); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(PublicationSaveViewModel model) {

            if (HttpContext.Session.Get("user") == null )
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else if (!ModelState.IsValid) 
            {
                return View("Index", new PublicationIndexViewModel
                {
                    UserClaim = User,
                    Svm = model,
                    Publications = Publications.FindAll(x => x.Profile.IdUser == User.FindFirstValue(ClaimTypes.NameIdentifier))
                }); ; ;
            
            }

           
                await _publicationServices.AddSaveViewModel(model);

             
            return View("Index", new PublicationIndexViewModel
            {
                UserClaim = User,
                Svm = new PublicationSaveViewModel
                {
                    IdProfile = User.FindFirstValue(ClaimTypes.NameIdentifier)
        },
                Publications = Publications.FindAll(x => x.Profile.IdUser == User.FindFirstValue(ClaimTypes.NameIdentifier))
            });





        }



        [HttpPost]
        public async Task<IActionResult> Update(PublicationSaveViewModel model)
        {

            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else if (!ModelState.IsValid)
            {
                return View("Index", new PublicationIndexViewModel
                {
                    UserClaim = User,
                    Svm = model,
                    Publications = Publications.FindAll(x => x.Profile.IdUser == User.FindFirstValue(ClaimTypes.NameIdentifier))
                }); ; ;

            }

            PublicationSaveViewModel svm = await _publicationServices.GetByIdSaveViewModel((int)model.Id);

            svm.VideoYoutube = model.VideoYoutube;
            svm.Title = model.Title;
            svm.Description = model.Description;

            if (model.Photo != null)
            {
                svm.Photo = model.Photo;
            }



            await _publicationServices.UpdateSaveViewModel(svm, (int)svm.Id);


            return RedirectToRoute(new { controller = "Publication", action = "Index" });

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _publicationServices.DeleteAsync(Id);

           _publicationServices.DeletePhotoFromStorage(Id);

            return View("Index", new PublicationIndexViewModel
            {
                UserClaim = User,
                Svm = new PublicationSaveViewModel
                {
                    IdProfile = User.FindFirstValue(ClaimTypes.NameIdentifier)
        },
                Publications = Publications.FindAll(x => x.Profile.IdUser == User.FindFirstValue(ClaimTypes.NameIdentifier))
            });
        }



        [HttpPost]
        public async Task<IActionResult> AddComment(CommentSaveViewModel svm)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else if (!ModelState.IsValid)
            {
 
                List<PublicationViewModel> list = Publications.FindAll(x => x.Profile.IdUser == userId);

                var publication = list.Find(x => x.Id == svm.IdPublication);
                if (publication != null)
                {
                    publication.svm = svm; // Assign the svm to the publication's Svm property
                }

                return View("Index", new PublicationIndexViewModel
                {
                    UserClaim = User,
                    Svm = new PublicationSaveViewModel
                    {
                        IdProfile = userId
                    },
                    Publications = list // Pass the list with the updated publication
                });
            }

            
            await _commentServices.AddSaveViewModel(svm);

       
            var publications = Publications.FindAll(x => x.Profile.IdUser == userId);



            return RedirectToRoute(new { controller = "Publication", action = "Index" });            
       
        }
    }






    
}

