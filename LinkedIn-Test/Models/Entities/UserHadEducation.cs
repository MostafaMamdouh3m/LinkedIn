using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class UserHadEducation
    {

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentEducation { get; set; }

        public string Degree { get; set; }        // add: by mostafa

        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Education")]
        public int Fk_Education { get; set; }
        public Education Education { get; set; }


    }
}