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
            uvm.User = context.Users.ToList()[0];                  // will be edited
            uvm.UserHadEducations = context.UserHadEducation.Where(e => e.Fk_User == uvm.User.Id).ToList();
            uvm.Countries = context.Countries.ToList();
            uvm.User.CurrentEducation = context.Educations.Find(uvm.User.Fk_CurrentEducation);


            List<Education> usereducations = new List<Education>();
            uvm.EducationsAll = context.Educations.ToList();
            uvm.Educations = usereducations;

            uvm.WorkplacesAll = context.Workplaces.ToList();    // Mesawy
            uvm.UserAtWorkplaces = context.UserAtWorkplace.Where(e => e.Fk_User == uvm.User.Id).ToList();

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
                olduser.Fk_CurrentEducation = user.Fk_CurrentEducation;
                olduser.CurrentEducation = user.CurrentEducation;
                olduser.Fk_Country = user.Fk_Country;
                olduser.Country = context.Countries.Find(user.Fk_Country);

                olduser.Workplaces = user.Workplaces;
                olduser.Educations = user.Educations;
                olduser.Skills = user.Skills;
                olduser.Friends = user.Friends;
                context.SaveChanges();

                UserViewModel uvm = new UserViewModel();
                uvm.User = user;
                uvm.User.CurrentEducation = context.Educations.Find(user.Fk_CurrentEducation);
                uvm.User.Country= context.Countries.Find(user.Fk_Country);
                uvm.Users = context.Users.ToList();
                uvm.UserHadEducations = context.UserHadEducation.ToList();
                uvm.EducationsAll = context.Educations.ToList();

                List<Education> usereducations = new List<Education>();                
                uvm.Educations = usereducations;

                for (int i = 0; i < uvm.UserHadEducations.Count; i++)
                {
                    int temp = uvm.UserHadEducations[i].Fk_Education;
                    uvm.Educations.Add(context.Educations.Where(e => e.Id == temp).ToList()[0]);
                }

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
                uvm.User = context.Users.ToList()[0];                           // will be edited
                userHadEducation.Fk_User = uvm.User.Id;
                context.UserHadEducation.Add(userHadEducation);
                context.SaveChanges();              
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
                return PartialView("_Partial_Add_Education");

        }

        [HttpPost]
        public ActionResult AddWorkplaceAjax(UserAtWorkplace userAtWorkplace)  //By: Mesawy
        {
            if (ModelState.IsValid)
            {

                context.UserAtWorkplace.Add(userAtWorkplace);
                context.SaveChanges();

                UserViewModel uvm = new UserViewModel();
                uvm.User = context.Users.FirstOrDefault();  //Select first user
                uvm.UserAtWorkplaces = context.UserAtWorkplace.Where(e => e.Fk_User == uvm.User.Id).ToList();   //All workplaces-relations For this user

                for (int i = 0; i < uvm.UserAtWorkplaces.Count; i++)
                {
                    int temp = uvm.UserAtWorkplaces[i].Fk_Workplace;
                    uvm.Workplaces.Add(context.Workplaces.Find(temp));
                }

                //foreach (UserAtWorkplace item in uvm.UserAtWorkplaces)
                //    uvm.Workplaces.Add(item.Workplace);

                return PartialView("_PartialExperience", uvm);
            }
            else
                return PartialView("_Partial_Add_Experience");
        }

        [HttpPost]
        public ActionResult EditEducationAjax(UserHadEducation userHadEducation)
        {
            userHadEducation.Id = 2;          // will be edited
            userHadEducation.Fk_User = context.Users.ToList()[0].Id;      // will be edited

            if (ModelState.IsValid)
            {
                UserViewModel uvm = new UserViewModel();
                uvm.UserHadEducation = userHadEducation;
                uvm.User = context.Users.ToList()[0];            // will be edited

                UserHadEducation old = context.UserHadEducation.Find(userHadEducation.Id);

                old.Activities = userHadEducation.Activities;
                old.CurrentEducation = userHadEducation.CurrentEducation;
                old.Degree = userHadEducation.Degree;
                old.Description = userHadEducation.Description;
                old.Education = userHadEducation.Education;
                old.EndDate = userHadEducation.EndDate;
                old.FieldOfStudy = userHadEducation.FieldOfStudy;
                old.Fk_Education = userHadEducation.Fk_Education;
                old.Fk_User = userHadEducation.Fk_User;
                old.Grade = userHadEducation.Grade;
                old.User = userHadEducation.User;
                old.StartDate = userHadEducation.StartDate;

                context.SaveChanges();

                uvm.UserHadEducation.Fk_User = userHadEducation.Fk_User;
                uvm.UserHadEducation.Fk_Education = userHadEducation.Fk_Education;
                uvm.UserHadEducation.Activities = userHadEducation.Activities;
                uvm.UserHadEducation.Degree = userHadEducation.Degree;
                uvm.UserHadEducation.Description = userHadEducation.Description;
                uvm.UserHadEducation.EndDate = userHadEducation.EndDate;
                uvm.UserHadEducation.FieldOfStudy = userHadEducation.FieldOfStudy;
                uvm.UserHadEducation.Grade = userHadEducation.Grade;
                uvm.UserHadEducation.StartDate = userHadEducation.StartDate;


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
                return PartialView("_Partial_Edit_Education");

            }
        }

    }
}