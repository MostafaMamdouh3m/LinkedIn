using LinkedIn_Test.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    [Table("UserSkill")]
    public class UserSkill
    {
        public int Id { get; set; }

        public int MyProperty { get; set; }

        public SkillLevel Level { get; set; }

        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Skill")]
        public int Fk_Skill { get; set; }
        public Skill Skill { get; set; }


    }
}