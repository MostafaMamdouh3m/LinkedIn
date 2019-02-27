using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Friend
    {

        public int Id { get; set; }
        public bool isAccepted { get; set; }

        [ForeignKey("UserOne")]
        public string Fk_UserOne { get; set; }
        public ApplicationUser UserOne { get; set; }


        [ForeignKey("UserTwo")]
        public string Fk_UserTwo { get; set; }
        public ApplicationUser UserTwo { get; set; }


    }
}