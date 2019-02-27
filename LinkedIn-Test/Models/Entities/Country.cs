﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Country
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<ApplicationUser> Users { get; set; }

    }
}