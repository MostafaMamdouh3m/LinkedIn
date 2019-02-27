using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class UserAtWorkplace
    {

        public int Id { get; set; }
        public string Postion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CurrentWork { get; set; }

        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Workplace")]
        public int Fk_Workplace { get; set; }
        public Workplace Workplace { get; set; }




    }
}