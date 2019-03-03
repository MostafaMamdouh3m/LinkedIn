﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Workplace
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Company*")]
        public string Name { get; set; }




        public List<ApplicationUser> Users { get; set; }
    }
}