using LinkedIn_Test.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkedIn_Test.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {

            if (User.Identity.Name == "" )
            {
                return Redirect("/Account/Register");
            }

            ViewBag.User = context.Users.Find(User.Identity.GetUserId());
            return View();
        }


        [HttpPost]
        public PartialViewResult Search(string str)
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

            return PartialView("_Partial_SearchResults", users);
        }

    }
}