using LinkedIn_Test.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{

    [Table("UserEducation")]
    public class UserEducation
    {

        public int Id { get; set; }

        // add: by mostafa
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        // add: by mostafa
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool CurrentEducation { get; set; }

        public string Degree { get; set; }          // add: by mostafa
        public string FieldOfStudy { get; set; }        // add: by mostafa
        public EducationGrade? Grade { get; set; }            // add: by mostafa
        public string Activities { get; set; }        // add: by mostafa
        public string Description { get; set; }        // add: by mostafa


        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Education")]
        public int Fk_Education { get; set; }
        public Education Education { get; set; }


    }
}