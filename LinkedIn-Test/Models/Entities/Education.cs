using LinkedIn_Test.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Education
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public EducationType Type { get; set; }


        public List<ApplicationUser> Users { get; set; }

    }
}