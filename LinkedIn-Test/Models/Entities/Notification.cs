using LinkedIn_Test.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    public class Notification
    {

        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public bool Seen { get; set; }
        public string Info { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public string Fk_User { get; set; }
        public ApplicationUser User { get; set; }

    }
}