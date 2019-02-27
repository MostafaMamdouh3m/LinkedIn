using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Comment
    {

        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public int LikeCount { get; set; }


        [ForeignKey("CommentOwner")]
        [Display(Name = "Comment Owner")]
        public string Fk_CommentOwner { get; set; }
        public ApplicationUser CommentOwner { get; set; }

    }
}