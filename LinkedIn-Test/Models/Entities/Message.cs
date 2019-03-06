using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.Models.Entities
{
    [Table("Message")]
    public class Message
    {

        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool Recived { get; set; }
        public bool Seen { get; set; }

        [ForeignKey("Sender")]
        public string Fk_Sender { get; set; }
        public ApplicationUser Sender { get; set; }


        [ForeignKey("Reciver")]
        public string Fk_Reciver { get; set; }
        public ApplicationUser Reciver { get; set; }

    }
}