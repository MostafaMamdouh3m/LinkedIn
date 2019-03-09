using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.ViewModels
{
    public class HomeViewModel
    {
        public List<Post> Posts { get; set; }
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public List<UserLikePost> Likes { get; set; }
        public ApplicationUser User { get; set; }
        public List<ApplicationUser> FriendsOfUser { get; set; }
        public List<Friend> Friends { get; set; }
        public List<ApplicationUser> FriendRequest { get; set; }
    }
}