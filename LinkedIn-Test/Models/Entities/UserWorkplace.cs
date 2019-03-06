using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{

    [Table("UserWorkplace")]
    public class UserWorkplace
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Head Line*")]
        public string HeadLine { get; set; }
        [Required]
        [Display(Name = "Start Date*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public bool CurrentWork { get; set; }
        [Required]
        [Display(Name = "Title*")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Industry*")]
        public string Industry { get; set; }

        public string Location { get; set; }
        //public string Company { get; set; } //Workplace.Name



        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Workplace")]
        public int Fk_Workplace { get; set; }
        public Workplace Workplace { get; set; }
    }
}