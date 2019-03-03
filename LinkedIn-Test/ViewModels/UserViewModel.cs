using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public List<ApplicationUser> Users { get; set; }

        public ApplicationUser User { get; set; }

        public List<Country> Countries { get; set; }

        public Education Education { get; set; }

        public List<Education> Educations { get; set; }

        public List<UserHadEducation> UserHadEducations { get; set; }

        public UserHadEducation UserHadEducation { get; set; }

        public List<Education> EducationsAll { get; set; }

        public Workplace Workplace { get; set; }

        public UserAtWorkplace UserAtWorkplace { get; set; }

        public List<Workplace> Workplaces { get; set; }

        public List<Workplace> WorkplacesAll { get; set; }

        public List<UserAtWorkplace> UserAtWorkplaces { get; set; }
    }
}