using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Post
    {

        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool isShared { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public string Media { get; set; }



        [ForeignKey("PostOwner")]
        [Display(Name = "Post Owner")]
        public string Fk_PostOwner { get; set; }
        public ApplicationUser PostOwner { get; set; }


        [ForeignKey("SharedPost")]
        public int? Fk_SharedPost { get; set; }
        public Post SharedPost { get; set; }


        public List<Comment> Comments { get; set; }

    }
}