using Microsoft.AspNetCore.Mvc;
using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Identity;
using System.Security.Claims;

namespace ShareItApp.Controllers
{
    public class FriendshipController : Controller
    {

        public readonly IPublicationServices _publicationServices;

        public readonly List<PublicationViewModel> Publications;

        public readonly ICommentServices _commentServices;

        public readonly IFriendshipRepository _friendshipRepository;

        



        public FriendshipController(IPublicationServices publicationServices, ICommentServices commentServices, IFriendshipRepository friendshipRepository)
        {
            _publicationServices = publicationServices;          
            _commentServices = commentServices;
            _friendshipRepository = friendshipRepository;
            Publications = _publicationServices.GetAllViewModel().Result;
        
        }

        public async Task<IActionResult> Index(FriendshipIndexViewModel vm)
        {
            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            else if (!ModelState.IsValid)
            {
                return View("Index", vm);

            }

            string Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(new FriendshipIndexViewModel
            {
                UserClaim = User,
                Svm = new FriendshipSaveViewModel()
                ,
                Publications = Publications.FindAll(x => x.Profile.IdUser == Id)

            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _friendshipRepository.DeleteAsync(Id);


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
        public async Task<IActionResult> Add(PublicationSaveViewModel model)
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
                    publication.svm = svm;
                }

                return View("Index", new PublicationIndexViewModel
                {
                    UserClaim = User,
                    Svm = new PublicationSaveViewModel
                    {
                        IdProfile = userId
                    },
                    Publications = list 
                });
            }


            await _commentServices.AddSaveViewModel(svm);


            var publications = Publications.FindAll(x => x.Profile.IdUser == userId);



            return RedirectToRoute(new { controller = "Friends", action = "Index" });

        }
    }
}
