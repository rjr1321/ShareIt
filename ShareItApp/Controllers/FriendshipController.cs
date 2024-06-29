using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShareIt.Core.Application;
using ShareIt.Core.Domain;
using ShareIt.Infrastructure.Identity;
using ShareIt.Infrastructure.Persistence;
using System.Security.Claims;

namespace ShareItApp.Controllers
{
    public class FriendshipController : Controller
    {

        public readonly IPublicationServices _publicationServices;

        public readonly ICommentServices _commentServices;

        public readonly IUserServices _userServices;

        public readonly IFriendshipRepository _friendshipRepository;

        public readonly UserManager<User> _userManager;

       

        public readonly List<PublicationViewModel> Publications;

        public readonly List<UserFriendshipViewModel> Users;


        public FriendshipController(IPublicationServices publicationServices, ICommentServices commentServices, IFriendshipRepository friendshipRepository, UserManager<User> userManager, IUserServices userServices)
        {
            _publicationServices = publicationServices;          
            _commentServices = commentServices;
            _friendshipRepository = friendshipRepository;
            _userManager = userManager;
            _userServices = userServices;

            Users = new List<UserFriendshipViewModel>();

            Publications = _publicationServices.GetAllViewModel().Result;       

        }

        private async Task LoadUsers()
        {
            try
            {
                string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

               
                List<AppProfile> profiles = await _userServices.GetAllAsync();
                List<Friendship> friendships = (await _friendshipRepository.GetAllAsync()).ToList();

               
                List<Friendship> userFriendships = friendships.FindAll(x => x.AppProfileId == loggedInUserId);

                Users.Clear();

             
                foreach (var user in _userManager.Users)
                {
                    if (user.Id != loggedInUserId) 
                    {
                        bool isFriend = userFriendships.Any(x => x.FriendId == user.Id);
                        AppProfile profile = profiles.FirstOrDefault(x => x.IdUser == user.Id);

                        Users.Add(new UserFriendshipViewModel
                        {
                            Id = user.Id,
                            Name = user.Name,
                            LastName = user.LastName,
                            Photo = profile?.PhotoProfile,
                            Username = user.UserName, 

                            svm = new FriendshipSaveViewModel
                            {
                                IdFriend = user.Id
                            },

                            Added = isFriend
                        });
                    }
                }
            }
            catch (Exception ex)
            {
          
                Console.WriteLine(ex.ToString());
            }
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

            await LoadUsers();
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch all friendships for the logged-in user
            List<Friendship> userFriendships = (await _friendshipRepository.GetAllAsync())
                .Where(x => x.AppProfileId == loggedInUserId || x.FriendId == loggedInUserId)
                .ToList();

            // Get friend IDs
            List<string> friendIds = userFriendships
                .Select(x => x.AppProfileId == loggedInUserId ? x.FriendId : x.AppProfileId)
                .ToList();

            // Fetch publications of friends
            List<PublicationViewModel> friendPublications = (await _publicationServices.GetAllViewModel())
                .Where(x => friendIds.Contains(x.Profile.IdUser))
                .ToList();

            return View(new FriendshipIndexViewModel
            {
                UserClaim = User,
                Svm = new FriendshipSaveViewModel(),
                Publications = friendPublications,
                Users = Users
            });
        }




        [HttpPost]
        public async Task<IActionResult> Delete(FriendshipIndexViewModel vm)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        

            await _friendshipRepository.DeleteAsync(loggedInUserId, vm.Svm.IdFriend);


            return RedirectToRoute(new { controller = "Friendship", action = "Index" });
        }


        [HttpPost]
        public async Task<IActionResult> Add(UserFriendshipViewModel model)
        {

            if (HttpContext.Session.Get("user") == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }


           AppProfile profile = await _userServices.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            AppProfile friend = await _userServices.GetByIdAsync(model.svm.IdFriend);

           if(profile.Friends == null)
            {
                profile.Friends = new List<Friendship>();
            }

            if (friend.Friends == null)
            {
                friend.Friends = new List<Friendship>();
            }

            profile.Friends.Add(new Friendship
            {
                AppProfileId = profile.IdUser,
                FriendId = model.svm.IdFriend

            });

            friend.Friends.Add(new Friendship
            {
                AppProfileId = model.svm.IdFriend,
                FriendId = profile.IdUser

            });

            await _userServices.UpdateAsync(profile, profile.IdUser);

            await _userServices.UpdateAsync(friend, friend.IdUser);



            return RedirectToRoute(new { controller = "Friendship", action = "Index" });

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



            return RedirectToRoute(new { controller = "Friendship", action = "Index" });

        }
    }
}
