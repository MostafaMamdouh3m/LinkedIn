using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkedIn_Test.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {

            if (User.Identity.Name == "" )
            {
                return Redirect("/Account/Register");
            }

          
            var currUserId = User.Identity.GetUserId();
            // Get user friends Posts   //++++++++++++++++++++++++++++++++++++++++//
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Posts = new List<Post>();
            homeViewModel.Comments = new List<Comment>();

            //homeViewModel.FriendsOfUser = new List<ApplicationUser>();// From DataBase
            var usersDummy = context.Users.ToList();
            homeViewModel.FriendsOfUser = usersDummy.Where(m => m.Id != currUserId).ToList();
            foreach (var userFriend in homeViewModel.FriendsOfUser)
            {
                userFriend.Posts = context.Posts.Where(m => m.Fk_PostOwner == userFriend.Id).ToList();
                if (userFriend.Posts != null)
                {
                    foreach (var post in userFriend.Posts)
                        post.Comments = context.Comments.Where(m => m.FK_postId == post.Id).ToList();
                    homeViewModel.Posts.AddRange(userFriend.Posts);
                }
            }

            homeViewModel.Posts.OrderByDescending(e => e.Date);
            ViewBag.User = context.Users.Find(User.Identity.GetUserId());

            return View(homeViewModel);
        }


        [HttpPost]
        public PartialViewResult Search(string str)
        {
            int spacesNum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt(i) == ' ')
                {
                    spacesNum++;
                }
            }

            int wordsNum = spacesNum + 1;
            string[] words = new string[wordsNum];

            int counter = 0;
            string temp = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt(i) != ' ')
                {
                    temp += str.ElementAt(i);
                }
                else
                {
                    words[counter] = temp;
                    counter++;
                    temp = "";
                }
            }
            words[counter] = temp;

            List<ApplicationUser> users;
            string temp_one;
            string temp_two;
            string temp_three;
            switch (wordsNum)
            {
                case 1:
                    temp_one = words[0];
                    users = context.Users.Where(e => e.FirstName.Contains(temp_one) || e.MiddleName.Contains(temp_one) || e.LastName.Contains(temp_one)).ToList();
                    break;
                case 2:
                    temp_one = words[0];
                    temp_two = words[1];
                    users = context.Users.Where(e => (e.FirstName.Contains(temp_one) && e.LastName.Contains(temp_two)) ||
                                                     (e.FirstName.Contains(temp_one) && e.MiddleName.Contains(temp_two)) ||
                                                     (e.MiddleName.Contains(temp_one) && e.LastName.Contains(temp_two))).ToList();
                    break;
                case 3:
                    temp_one = words[0];
                    temp_two = words[1];
                    temp_three = words[2];
                    users = context.Users.Where(e => e.FirstName.Contains(temp_one) && e.MiddleName.Contains(temp_two) && e.LastName.Contains(temp_three)).ToList();
                    break;
                default:
                    users = null;
                    break;
            }

            return PartialView("_Partial_SearchResults", users);
        }

        [HttpPost]
        public PartialViewResult AjaxAddPost(Post post)
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = context.Users.SingleOrDefault(x => x.Id == currUserId);
            post.Fk_PostOwner = currUserId;
            post.PostOwner = currUser;

            context.Posts.Add(post);
            context.SaveChanges();

            //HomeViewModel homeViewModel = new HomeViewModel();
            //homeViewModel.Post = post;
            //homeViewModel.User = currUser;
            //homeViewModel.Posts = context.Posts.Where(m => m.Fk_PostOwner == currUserId).ToList();

            return PartialView("_Partial_Post", post);
        }

        [HttpPost]
        public PartialViewResult AjaxAddComment(Comment Comment)
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = context.Users.SingleOrDefault(x => x.Id == currUserId);

            Comment.CommentOwner = currUser;
            Comment.Fk_CommentOwner = currUserId;
            Comment.Post = context.Posts.Find(Comment.FK_postId);

            context.Comments.Add(Comment);
            context.SaveChanges();

            return PartialView("_PartialComment", Comment);
        }

        [HttpPost]
        public void AjaxAddLike(UserLikePost userLikePost)
        {

            var currUserId = User.Identity.GetUserId();
            var currUser = context.Users.SingleOrDefault(e => e.Id == currUserId);

            userLikePost.Fk_User = currUserId;
            var post = context.Posts.SingleOrDefault(e => e.Id == userLikePost.Fk_Post);

            if ( context.UserLikePost.Where(p => p.Fk_Post == post.Id && p.Fk_User == currUserId)== null)
            {
                context.UserLikePost.Find(post.Id);
                post.LikeCount++;
                context.UserLikePost.Add(userLikePost);
                context.SaveChanges();
            }
            else
            {
                post.LikeCount--;
                context.UserLikePost.Remove(userLikePost);
                context.SaveChanges();
            }
          
        }
    }
}