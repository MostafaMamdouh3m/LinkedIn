using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkedIn_Test.Controllers
{
    public class MessagesController : Controller
    {

        ApplicationDbContext context;
        MessageViewModel viewModel;
        string userId;

        public MessagesController()
        {
            context = new ApplicationDbContext();
            viewModel = new MessageViewModel();
        }


        [HttpGet] // Done
        public ActionResult Index()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Account/Register");
            }

            userId = User.Identity.GetUserId();

            List<Message> msgs = context.Messages.Include("Sender").Include("Reciver").OrderByDescending(e => e.Date).Where(e => e.Sender.Id == userId || e.Reciver.Id == userId).Take(100).ToList();
            viewModel.lastMessages = GetChatsCount(msgs, userId);
            if (viewModel.lastMessages.Count != 0)
            {
                string key = viewModel.lastMessages[0].Key;
                viewModel.Chat = context.Messages.OrderByDescending(e => e.Date).Where(e => (e.Sender.Id == userId || e.Reciver.Id == userId) && (e.Sender.Id == key || e.Reciver.Id == key)).Take(20).ToList();
            }

            // Set Message Updates to false cause of retrieving them
            // Set Messages Recived attribute to true
            context.Users.Where(e => e.UserName == User.Identity.Name).ToArray()[0].MessageUpdated = false;
            for (int i = 0; i < msgs.Count; i++)
            {
                msgs[i].Recived = true;
            }
            context.SaveChanges();

            // ViewBag for filling the Nav_Bar User details
            ViewBag.User = context.Users.Find(User.Identity.GetUserId());
            if (viewModel.lastMessages.Count > 0)
            {
                string ChatInfoKey = viewModel.lastMessages[0].Key;
                ViewBag.ChatInfo = context.Users.Where(e => e.Id == ChatInfoKey).ToArray()[0];
            }
            return View(viewModel);
        }

        [HttpPost] // Done
        public void AjaxSendMessage(Message msg)
        {
            msg.Recived = false;
            msg.Seen = false;
            context.Messages.Add(msg);
            context.Users.Where(e => e.Id == msg.Fk_Reciver).ToArray()[0].MessageUpdated = true;
            context.SaveChanges();
        }

        [HttpPost]
        public PartialViewResult AjaxLoadMoreMessages()
        {
            return null;
        }

        [HttpPost]
        public PartialViewResult AjaxLoadMoreChats(int currentChatsNumber)
        {
            int trial = 0;
            List<Message> msgs = context.Messages.OrderByDescending(e => e.Date).Where(e => e.Sender.Id == userId || e.Reciver.Id == userId).ToList();
            List<Message> temp;
            do
            {
                trial++;
                userId = User.Identity.GetUserId();
                temp = context.Messages.Include("Sender").Include("Reciver").OrderByDescending(e => e.Date).Where(e => e.Sender.Id == userId || e.Reciver.Id == userId).ToList();
                viewModel.lastMessages = GetChatsCount(msgs, userId);

            } while (viewModel.lastMessages.Count <= currentChatsNumber && temp.Count < msgs.Count);

            if (viewModel.lastMessages.Count == currentChatsNumber)
            {
                return null;
            }
            else
            {
                return PartialView("_PartialMessageUpdatePanes", viewModel.lastMessages);
            }
        }

        [HttpPost] // Done
        public PartialViewResult AjaxLoadChat(string Id)
        {
            userId = User.Identity.GetUserId();

            viewModel.Chat = context.Messages.Include("Sender").Include("Reciver").OrderByDescending(e => e.Date).Where(e => (e.Sender.Id == userId || e.Reciver.Id == userId) && (e.Sender.Id == Id || e.Reciver.Id == Id)).Take(20).ToList();
            return PartialView("_PartialMessageBoard", viewModel.Chat);
        }

        [HttpPost] // Done
        public bool CheckChats()
        {
            if (context.Users.Find(User.Identity.GetUserId()) == null)
            {
                return false;
            }
            return context.Users.Find(User.Identity.GetUserId()).MessageUpdated;
        }

        [HttpPost] // Done
        public PartialViewResult UpdateCurrentChat(string Id)
        {
            List<Message> msgs = context.Messages.Include("Sender").Include("Reciver").OrderByDescending(e => e.Date).Where(e => (e.Sender.UserName == User.Identity.Name || e.Reciver.UserName == User.Identity.Name) && (e.Sender.Id == Id || e.Reciver.Id == Id)).Take(50).ToList();
            for (int i = 0; i < msgs.Count; i++)
            {
                if (msgs[i].Sender.UserName == User.Identity.Name && !msgs[i].Recived)
                {
                    for (int j = msgs.Count - 1; j >= i; j--)
                    {
                        msgs.RemoveAt(i);
                    }
                    break;
                }
            }
            for (int i = 0; i < msgs.Count; i++)
            {
                msgs[i].Recived = true;
            }

            context.Users.Where(e => e.UserName == User.Identity.Name).ToArray()[0].MessageUpdated = false;
            context.SaveChanges();

            return PartialView("_PartialMessagesBundle", msgs);
        }

        [HttpPost] //Done
        public PartialViewResult UpdateChats()
        {
            userId = User.Identity.GetUserId();

            List<Message> msgs = context.Messages.Include("Sender").Include("Reciver").OrderByDescending(e => e.Date).Where(e => e.Sender.UserName == User.Identity.Name || e.Reciver.UserName == User.Identity.Name).Take(100).ToList();
            for (int i = 0; i < msgs.Count; i++)
            {
                msgs[i].Recived = true;
            }
            context.SaveChanges();
            viewModel.lastMessages = GetChatsCount(msgs, userId);

            return PartialView("_PartialMessageUpdatePanes", viewModel.lastMessages);
        }

        [HttpPost] //Done
        public PartialViewResult GetChatInfo(string Id)
        {
            ViewBag.ChatInfo = context.Users.Where(e => e.Id == Id).ToArray()[0];
            return PartialView("_PartialMessageBoardTitle");
        }

        [HttpPost] //Done
        public PartialViewResult SearchMessages(string str)
        {
            int spacesNum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt(i) == ' ')
                {
                    spacesNum++;
                }
            }

            int wordsNum = spacesNum + 1;
            string[] words = new string[wordsNum];

            int counter = 0;
            string temp = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt(i) != ' ')
                {
                    temp += str.ElementAt(i);
                }
                else
                {
                    words[counter] = temp;
                    counter++;
                    temp = "";
                }
            }
            words[counter] = temp;

            List<Message> tempMessages = new List<Message>();
            for (int i = 0; i < wordsNum; i++)
            {
                string tempWord = words[i];
                tempMessages.AddRange(context.Messages.Include("Sender").Include("Reciver").Where(e => e.Body.Contains(tempWord)).ToList());
            }

            List<Message> messages = new List<Message>();
            for (int i = 0; i < tempMessages.Count; i++)
            {
                if (!messages.Contains(tempMessages[i]))
                {
                    messages.Add(tempMessages[i]);
                }
            }
            viewModel.lastMessages = GetChatsCount(messages, User.Identity.GetUserId());

            return PartialView("_PartialMessageUpdatePanes", viewModel.lastMessages);
        }

        [HttpPost] //Done
        public PartialViewResult SearchUsers(string str)
        {
            int spacesNum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt(i) == ' ')
                {
                    spacesNum++;
                }
            }

            int wordsNum = spacesNum + 1;
            string[] words = new string[wordsNum];

            int counter = 0;
            string temp = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str.ElementAt(i) != ' ')
                {
                    temp += str.ElementAt(i);
                }
                else
                {
                    words[counter] = temp;
                    counter++;
                    temp = "";
                }
            }
            words[counter] = temp;

            List<ApplicationUser> users;
            string temp_one;
            string temp_two;
            string temp_three;
            switch (wordsNum)
            {
                case 1:
                    temp_one = words[0];
                    users = context.Users.Where(e => e.FirstName.Contains(temp_one) || e.MiddleName.Contains(temp_one) || e.LastName.Contains(temp_one)).ToList();
                    break;
                case 2:
                    temp_one = words[0];
                    temp_two = words[1];
                    users = context.Users.Where(e => (e.FirstName.Contains(temp_one) && e.LastName.Contains(temp_two)) ||
                                                     (e.FirstName.Contains(temp_one) && e.MiddleName.Contains(temp_two)) ||
                                                     (e.MiddleName.Contains(temp_one) && e.LastName.Contains(temp_two))).ToList();
                    break;
                case 3:
                    temp_one = words[0];
                    temp_two = words[1];
                    temp_three = words[2];
                    users = context.Users.Where(e => e.FirstName.Contains(temp_one) && e.MiddleName.Contains(temp_two) && e.LastName.Contains(temp_three)).ToList();
                    break;
                default:
                    users = null;
                    break;
            }

            users.Remove(context.Users.Find(User.Identity.GetUserId()));
            return PartialView("_PartialUsersToMessages", users);
        }

        public PartialViewResult StartNewMessage(string Id)
        {
            List<Message> msgs = context.Messages.Include("Sender").Include("Reciver").OrderByDescending(e => e.Date).Where(e => (e.Sender.UserName == User.Identity.Name || e.Reciver.UserName == User.Identity.Name) && (e.Sender.Id == Id || e.Reciver.Id == Id)).Take(1).ToList();
            if (msgs.Count > 0)
            {
                return PartialView("_PartialMessage", msgs[0]);
            }
            return PartialView("_PartialNewMessage", context.Users.Find(Id));
        }

        #region Private methods

        private List<KeyValuePair<string, Message>> GetChatsCount(List<Message> msgs, string userId)
        {
            List<string> users = new List<string>();
            List<KeyValuePair<string, Message>> messages = new List<KeyValuePair<string, Message>>();

            for (int i = 0; i < msgs.Count; i++)
            {
                if (!users.Contains(msgs[i].Reciver.Id) && userId != msgs[i].Reciver.Id)
                {
                    messages.Add(new KeyValuePair<string, Message>(msgs[i].Reciver.Id, msgs[i]));
                    users.Add(msgs[i].Reciver.Id);
                }
                if (!users.Contains(msgs[i].Sender.Id) && userId != msgs[i].Sender.Id)
                {
                    messages.Add(new KeyValuePair<string, Message>(msgs[i].Sender.Id, msgs[i]));
                    users.Add(msgs[i].Sender.Id);
                }
            }
            return messages;
        }
        #endregion


    }
}