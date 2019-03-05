using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LinkedIn_Test.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        UserViewModel viewModel = new UserViewModel();

        public ActionResult Index()                           //by: mostafa
        {
           //List<ApplicationUser>  users = context.Users.ToList();

            string userId = User.Identity.GetUserId();
            viewModel.User = context.Users.Where(e => e.Id == userId).ToList()[0];
            viewModel.User.UserEductions = context.UserEducations.Where(e => e.Fk_User == userId).ToList();
            viewModel.Educations = context.Educations.ToList();
            viewModel.Countries = context.Countries.ToList();

            viewModel.DropDownListForEducationsOfUser = new List<Education>();

            for (int i = 0; i < viewModel.User.UserEductions.Count; i++)
            {
                int temp = viewModel.User.UserEductions[i].Fk_Education;
                viewModel.DropDownListForEducationsOfUser.Add(context.Educations.Where(e => e.Id == temp).ToList()[0]);
            }

            ViewBag.User = context.Users.Find(User.Identity.GetUserId());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditHeaderFormAjex(ApplicationUser User)                   //by: mostafa
        {

            if (ModelState.IsValid)
            {
                ApplicationUser olduser = context.Users.Find(User.Id);
                olduser = User;

                olduser.FirstName = User.FirstName;
                olduser.LastName = User.LastName;
                olduser.Headline = User.Headline;
                olduser.CurrentPosition = User.CurrentPosition;
                olduser.ProfilePicture = User.ProfilePicture;
                olduser.HeaderPicture = User.HeaderPicture;
                olduser.Summary = User.Summary;
                olduser.CurrentPosition = User.CurrentPosition;
                olduser.Fk_CurrentEducation = User.Fk_CurrentEducation;
                olduser.CurrentEducation = User.CurrentEducation;
                olduser.Fk_Country = User.Fk_Country;
                olduser.Country = context.Countries.Find(User.Fk_Country);
                context.SaveChanges();

                viewModel.User = User;
                viewModel.User.CurrentEducation = context.Educations.Find(User.Fk_CurrentEducation);
                viewModel.User.Country = context.Countries.Find(User.Fk_Country);
                viewModel.Users = context.Users.ToList();
                viewModel.User.UserEductions = context.UserEducations.Where(e => e.Fk_User == User.Id).ToList();
                viewModel.Educations= context.Educations.ToList();

                return PartialView("_Partial_Profile_Header_Work_Education", viewModel);
            }

            else
            {
                return PartialView("_Partial_Profile_EditHeaderForm");

            }
        }

        [HttpPost]
        public ActionResult AddWorkplaceAjax(UserWorkplace userAtWorkplace)  //By: Mesawy
        {

            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                context.UserWorkplaces.Add(userAtWorkplace);
                context.SaveChanges();

                viewModel.User.UserWorkplaces = context.UserWorkplaces.Include("Workplace").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_PartialExperience", viewModel);
            }
            else
                return PartialView("_Partial_Add_Experience");
        }
        [HttpPost]
        public ActionResult AddEducationAjax(UserEducation userEducation)
        {

            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                userEducation.Fk_User = userId;
                context.UserEducations.Add(userEducation);
                context.SaveChanges();
                viewModel.User = context.Users.Find(userId);
                viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);
            }
            else
                return PartialView("_Partial_Add_Education");

        }

        [HttpGet]
        public ActionResult EditEducationAjax(int id)
        {

            viewModel.UserEducation = context.UserEducations.Include("Education").Where(e => e.Id == id).ToArray()[0];
            viewModel.Educations = context.Educations.ToList();
            viewModel.Education = context.Educations.Where(e => e.Id == viewModel.UserEducation.Fk_Education).ToList()[0];
            viewModel.Education.Name = context.Educations.Where(e => e.Id == viewModel.UserEducation.Fk_Education).ToList()[0].Name;
            viewModel.Education.Type = context.Educations.Where(e => e.Id == viewModel.UserEducation.Fk_Education).ToList()[0].Type;
            viewModel.UserEducation.StartDate = context.UserEducations.Include("Education").Where(e => e.Id == id).ToArray()[0].StartDate;
            viewModel.UserEducation.EndDate = context.UserEducations.Include("Education").Where(e => e.Id == id).ToArray()[0].EndDate;



            return PartialView("_Partial_Edit_Education", viewModel);
        }
        [HttpPost]
        public ActionResult EditEducationAjax(UserEducation userEducation)
        {
            string userId = User.Identity.GetUserId();
            userEducation.Fk_User = userId;
            userEducation.Education.Type = context.Educations.Where(e => e.Id == userEducation.Fk_Education).ToList()[0].Type;
            userEducation.Education.Name = context.Educations.Where(e => e.Id == userEducation.Fk_Education).ToList()[0].Name;

            UserEducation oldEducation = context.UserEducations.Find(userEducation.Id);
            oldEducation = userEducation;

            context.UserEducations.Find(userEducation.Id).Activities = userEducation.Activities;
            oldEducation.Degree = userEducation.Degree;
            context.UserEducations.Find(userEducation.Id).Description = userEducation.Description;
            context.UserEducations.Find(userEducation.Id).FieldOfStudy = userEducation.FieldOfStudy;
            oldEducation.Grade = userEducation.Grade;
            oldEducation.StartDate = userEducation.StartDate;
            oldEducation.EndDate = userEducation.EndDate;
            context.UserEducations.Find(userEducation.Id).Fk_Education = userEducation.Fk_Education;
            oldEducation.Fk_User = userEducation.Fk_User;
            oldEducation.Education.Name = userEducation.Education.Name;
            oldEducation.Education.Type = userEducation.Education.Type;

            //context.Entry(oldEducation).State = EntityState.Modified;
            context.SaveChanges();

            viewModel.User = context.Users.Where(e => e.Id == userEducation.Fk_User).ToArray()[0];

         
            viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
            return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);

        }

        [HttpGet]
        public ActionResult DeleteEducationAjax(int id)
        {
            string userId = User.Identity.GetUserId();

            viewModel.User = context.Users.Where(e => e.Id == userId).ToList()[0];
            viewModel.UserEducation = context.UserEducations.Include("Education").Where(e => e.Id == id).ToArray()[0];
            viewModel.Education = context.Educations.Where(e => e.Id == viewModel.UserEducation.Fk_Education).ToList()[0];

            return PartialView("_Partial_Delete_Education", viewModel);
        }
        [HttpPost]
        public ActionResult DeleteEducationAjax(UserEducation userEducation)
        {
            string userId = User.Identity.GetUserId();
            userEducation.Fk_User = userId;

            context.UserEducations.Remove(userEducation);
            
            context.SaveChanges();

            viewModel.User = context.Users.Where(e => e.Id == userEducation.Fk_User).ToArray()[0];
            viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
            return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);

        }

        public ActionResult UserProfile(string Id)
        {
            return null;
        }
    }
}


