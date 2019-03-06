using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            viewModel.User.UserSkills = context.UserSkills.Where(e => e.Fk_User == userId).ToList();

            viewModel.Educations = context.Educations.ToList();
            viewModel.Skills = context.Skills.ToList();
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
                //olduser = User;

                olduser.FirstName = User.FirstName;
                olduser.LastName = User.LastName;
                olduser.Headline = User.Headline;
                olduser.CurrentPosition = User.CurrentPosition;
                olduser.Summary = User.Summary;
                olduser.CurrentPosition = User.CurrentPosition;
                olduser.Fk_CurrentEducation = User.Fk_CurrentEducation;
                olduser.CurrentEducation = User.CurrentEducation;
                olduser.Fk_Country = User.Fk_Country;
                olduser.Country = context.Countries.Find(User.Fk_Country);
                context.SaveChanges();

                viewModel.User = context.Users.Find(User.Id);
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

            oldEducation.Activities = userEducation.Activities;
            oldEducation.Degree = userEducation.Degree;
            oldEducation.Description = userEducation.Description;
            oldEducation.FieldOfStudy = userEducation.FieldOfStudy;
            oldEducation.Grade = userEducation.Grade;
            oldEducation.StartDate = userEducation.StartDate;
            oldEducation.EndDate = userEducation.EndDate;
            oldEducation.Fk_Education = userEducation.Fk_Education;
            oldEducation.Fk_User = userEducation.Fk_User;
            oldEducation.Education= context.Educations.Find(userEducation.Fk_Education);
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


            return PartialView("_Partial_Delete_Education", viewModel);
        }
        [HttpPost]
        public ActionResult DeleteEducationAjax(UserEducation userEducation)
        {
            string userId = User.Identity.GetUserId();
            userEducation.Fk_User = userId;

            context.UserEducations.Attach(userEducation);

            context.UserEducations.Remove(userEducation);
            
            context.SaveChanges();

            viewModel.User = context.Users.Find(userId);
            viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();

            return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);
        }

        [HttpPost]
        public ActionResult AddSkillAjax(Skill skill)
        {

            string userId = User.Identity.GetUserId();
            Skill existedSkill = new Skill();

            if (ModelState.IsValid)
            {
                // Skill database
                bool skillIsExistedInDB = false;

                for (int i=0;i<context.Skills.ToList().Count;i++)
                {
                    if (skill.Name == context.Skills.ToList()[i].Name)
                    {
                        skillIsExistedInDB = true;
                    }
                }
                if (skillIsExistedInDB == false)
                {
                    context.Skills.Add(skill);
                }
               
                else
                {
                   existedSkill = context.Skills.Where(e => e.Name == skill.Name).ToList()[0];
                }
                context.SaveChanges();

                // UserSkill database

                UserSkill userSkill = new UserSkill();
                userSkill.Fk_User = userId;

                if (skillIsExistedInDB == false)
                {
                    userSkill.Fk_Skill = skill.Id;
                }
                else
                {
                    userSkill.Fk_Skill = existedSkill.Id;
                }
              
                context.UserSkills.Add(userSkill);

                context.SaveChanges();
                viewModel.User = context.Users.Find(userId);
                viewModel.User.UserSkills = context.UserSkills.Include("Skill").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Skill_Data", viewModel.User.UserSkills);
            }
            else
                return PartialView("_Partial_Add_Skill", viewModel);

        }


        [HttpGet]
        public ActionResult EditSkillAjax(int id)
        {

            viewModel.UserSkill = context.UserSkills.Include("Skill").Where(e => e.Id == id).ToArray()[0];
            viewModel.Skills = context.Skills.ToList();
            viewModel.Skill = context.Skills.Where(e => e.Id == viewModel.UserSkill.Fk_Skill).ToList()[0];
            viewModel.Skill.Name = context.Skills.Where(e => e.Id == viewModel.UserSkill.Fk_Skill).ToList()[0].Name;
           
            return PartialView("_Partial_Edit_Skill", viewModel);
        }

        [HttpPost]
        public ActionResult EditSkillAjax(UserSkill userSkill)
        {
            //userSkill.Skill = context.Skills.Where(e => e.Id == userSkill.Fk_Skill).ToList()[0];

            if (ModelState.IsValid)
            {
                // Skill database
                string userId = User.Identity.GetUserId();
                userSkill.Fk_User = userId;

                Skill newSkill = new Skill();
                bool skillIsExistedInDB = false;
               
                for (int i = 0; i < context.Skills.ToList().Count; i++)
                {
                    if (userSkill.Skill.Name == context.Skills.ToList()[i].Name)
                    {
                        skillIsExistedInDB = true;
                    }
                }
                if (skillIsExistedInDB == false)
                {
                    newSkill = userSkill.Skill;
                    context.Skills.Add(newSkill);
                }
                context.SaveChanges();

                // UserSkill database

                UserSkill oldSkill = context.UserSkills.Where(e => e.Id == userSkill.Id).ToList()[0];
                oldSkill.Fk_User = userId;
                oldSkill.Skill.Name = userSkill.Skill.Name;


                if (skillIsExistedInDB == false)
                {
                    oldSkill.Fk_Skill = newSkill.Id;

                }
                else
                {
                    oldSkill.Fk_Skill = context.Skills.Where(e => e.Name == userSkill.Skill.Name).ToList()[0].Id;
                }
                context.SaveChanges();

                viewModel.User = context.Users.Where(e => e.Id == userSkill.Fk_User).ToArray()[0];

                viewModel.User.UserSkills = context.UserSkills.Include("Skill").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Skill_Data", viewModel.User.UserSkills);
            }
            else
            {
                return PartialView("_Partial_Edit_Skill", viewModel.User.UserSkills);
            }
            

        }


        [HttpGet]
        public ActionResult DeleteSkillAjax(int id)
        {
            string userId = User.Identity.GetUserId();

            viewModel.User = context.Users.Where(e => e.Id == userId).ToList()[0];
            viewModel.UserSkill = context.UserSkills.Include("Skill").Where(e => e.Id == id).ToArray()[0];

            return PartialView("_Partial_Delete_Skill", viewModel);
        }

        [HttpPost]
        public ActionResult DeleteSkillAjax(UserSkill userSkill)
        {
            string userId = User.Identity.GetUserId();
            userSkill.Fk_User = userId;

            context.UserSkills.Attach(userSkill);

            context.UserSkills.Remove(userSkill);

            context.SaveChanges();

            viewModel.User = context.Users.Find(userId);
            viewModel.User.UserSkills = context.UserSkills.Include("Skill").Where(e => e.Fk_User == userId).ToList();
            return PartialView("_Partial_Skill_Data", viewModel.User.UserSkills);
        }

        [HttpPost]
        public ActionResult UploadProfilePictureAjax(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string path = Path.Combine(Server.MapPath("~/content/images"), upload.FileName);
                upload.SaveAs(path);

                var currUserId = User.Identity.GetUserId();
                var currUser = context.Users.SingleOrDefault(e => e.Id == currUserId);

                currUser.ProfilePicture = upload.FileName;
                context.SaveChanges();

                return Json("/content/images/" + upload.FileName);
            }
            return null;
        }


    }
}


