using LinkedIn_Test.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkedIn_Test.Controllers
{
    public class NotificationController : Controller
    {
        ApplicationDbContext context;

        public NotificationController()
        {
            context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            if (User.Identity.Name == "")
            {
                return Redirect("/Account/Register");
            }

            ViewBag.User = context.Users.Find(User.Identity.GetUserId());
            return View();
        }
    }
}