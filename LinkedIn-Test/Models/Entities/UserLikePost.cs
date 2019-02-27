using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class UserLikePost
    {

        public int Id { get; set; }

        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Post")]
        public int Fk_Post { get; set; }
        public Post Post { get; set; }

    }
}