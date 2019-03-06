using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    [Table("Country")]
    public class Country
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<ApplicationUser> Users { get; set; }

    }
}