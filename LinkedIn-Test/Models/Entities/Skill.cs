using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Skill
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<ApplicationUser> Users { get; set; }

    }
}