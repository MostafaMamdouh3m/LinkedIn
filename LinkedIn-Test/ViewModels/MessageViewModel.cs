using LinkedIn_Test.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkedIn_Test.ViewModels
{
    public class MessageViewModel
    {
        public List<KeyValuePair<string, Message>> lastMessages;
        public List<Message> Chat;
    }
}