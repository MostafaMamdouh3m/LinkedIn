using LinkedIn_Test.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    [Table("Education")]
    public class Education
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]

        public EducationType Type { get; set; }


        public List<ApplicationUser> Users { get; set; }

    }
}