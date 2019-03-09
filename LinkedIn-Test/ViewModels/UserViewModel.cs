using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public UserEducation UserEducation { get; set; }
        public UserSkill UserSkill { get; set; }
        public UserWorkplace UserWorkplace { get; set; }

        // Database Section
        public List<ApplicationUser> Users { get; set; }
        public List<Country> Countries { get; set; }
        public Education Education { get; set; }
        public List<Education> Educations { get; set; }
        public Workplace Workplace { get; set; }
        public List<Workplace> Workplaces { get; set; }


        public Skill Skill{ get; set; }
        public List<Skill> Skills { get; set; }
        public List<Education> DropDownListForEducationsOfUser { get; set; }


        public bool  IsCurrentUserPage { get; set; }
    }
}