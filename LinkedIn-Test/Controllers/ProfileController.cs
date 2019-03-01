using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkedIn_Test.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult UserProfile()                           //by: mostafa
        {
            UserViewModel uvm = new UserViewModel();
            uvm.User = context.Users.ToList()[0];
            uvm.UserHadEducations = context.UserHadEducation.Where(e => e.Fk_User == uvm.User.Id).ToList();
            List<Education> usereducations = new List<Education>();
            uvm.EducationsAll = context.Educations.ToList();
            uvm.Educations = usereducations;

            for (int i = 0; i < uvm.UserHadEducations.Count; i++)
            {
                int temp = uvm.UserHadEducations[i].Fk_Education;
                uvm.Educations.Add(context.Educations.Where(e => e.Id == temp).ToList()[0]);
            }

            return View(uvm);
        }


        [HttpPost]
        public ActionResult EditHeaderFormAjex(ApplicationUser user)                   //by: mostafa
        {
            if (ModelState.IsValid)
            {
                ApplicationUser olduser = context.Users.Find(user.Id);
                olduser.FirstName = user.FirstName;
                olduser.LastName = user.LastName;
                olduser.Headline = user.Headline;
                olduser.CurrentPosition = user.CurrentPosition;
                olduser.ProfilePicture = user.ProfilePicture;
                olduser.HeaderPicture = user.HeaderPicture;
                olduser.Summary = user.Summary;
                olduser.CurrentPosition = user.CurrentPosition;
                olduser.Workplaces = user.Workplaces;
                olduser.Educations = user.Educations;
                olduser.Skills = user.Skills;
                olduser.Friends = user.Friends;
                context.SaveChanges();

                UserViewModel uvm = new UserViewModel();
                uvm.User = user;
                uvm.Users = context.Users.ToList();
                uvm.Educations = context.Educations.ToList();
                uvm.UserHadEducations = context.UserHadEducation.ToList();


                return PartialView("_Partial_Profile_Header_Work_Education", uvm);
            }

            else
            {
                return PartialView("_Partial_Profile_EditHeaderForm");

            }
        }

        [HttpPost]
        public ActionResult AddEducationAjax(UserHadEducation userHadEducation)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = new UserViewModel();
                context.UserHadEducation.Add(userHadEducation);
                context.SaveChanges();
                uvm.User = context.Users.ToList()[0];
                uvm.UserHadEducations = context.UserHadEducation.Where(e => e.Fk_User == uvm.User.Id).ToList();
                List<Education> educations = new List<Education>();
                uvm.Educations = educations;

                for (int i = 0; i < uvm.UserHadEducations.Count; i++)
                {
                    int temp = uvm.UserHadEducations[i].Fk_Education;
                    uvm.Educations.Add(context.Educations.Where(e => e.Id == temp).ToList()[0]);
                }

                return PartialView("_Partial_Education_Data", uvm);
            }

            else
            {
                return PartialView("_Partial_Add_Education");

            }

        }
    }
}