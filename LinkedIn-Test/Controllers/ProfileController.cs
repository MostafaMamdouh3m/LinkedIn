using LinkedIn_Test.Models;
using LinkedIn_Test.Models.Entities;
using LinkedIn_Test.Models.Enums;
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
            if (User.Identity.Name == "")
            {
                return Redirect("/account/register");
            }

            string userId = User.Identity.GetUserId();
            viewModel.User = context.Users.Find(userId);
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

            ViewBag.User = viewModel.User;
            ViewBag.IsCurrentUserPage = true;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditHeaderFormAjex(ApplicationUser User)                   //by: mostafa
        {
            ViewBag.IsCurrentUserPage = true;

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
                viewModel.User = context.Users.Find(User.Id);
                viewModel.User.FirstName = viewModel.User.FirstName;
                viewModel.User.LastName = viewModel.User.LastName;

                viewModel.User.CurrentEducation = context.Educations.Find(User.Fk_CurrentEducation);
                viewModel.User.Country = context.Countries.Find(User.Fk_Country);
                viewModel.Users = context.Users.ToList();
                viewModel.User.UserEductions = context.UserEducations.Where(e => e.Fk_User == User.Id).ToList();
                viewModel.Educations = context.Educations.ToList();
                return PartialView("_Partial_Profile_Header_Work_Education", viewModel);

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
        public ActionResult AddEducationAjax(UserViewModel userViewModel)
        {
            ViewBag.IsCurrentUserPage = true;
            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                //Education DB                    
                Education existedEducation = new Education();
                Education newEducation = new Education();


                if (ModelState.IsValid)
                {

                    bool educationIsExistedInDB = false;

                    for (int i = 0; i < context.Educations.ToList().Count; i++)
                    {
                        if (userViewModel.Education.Name == context.Educations.ToList()[i].Name && userViewModel.Education.Type == context.Educations.ToList()[i].Type)
                        {
                            educationIsExistedInDB = true;
                        }
                    }
                    if (educationIsExistedInDB == false)
                    {
                        newEducation.Name = userViewModel.Education.Name;
                        newEducation.Type = userViewModel.Education.Type;
                        context.Educations.Add(newEducation);
                    }

                    else
                    {
                        existedEducation = context.Educations.Where(e => e.Name == userViewModel.Education.Name).ToList()[0];
                    }
                    context.SaveChanges();


                    // userEducation DB
                    userViewModel.UserEducation.Fk_User = userId;
                    if (educationIsExistedInDB == false)
                    {
                        userViewModel.UserEducation.Fk_Education = newEducation.Id;
                    }
                    else
                    {
                        userViewModel.UserEducation.Fk_Education = existedEducation.Id;
                    }
                    context.UserEducations.Add(userViewModel.UserEducation);

                    context.SaveChanges();
                    viewModel.User = context.Users.Find(userId);
                    viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
                }
               
                return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);
            }
            else
            {
                viewModel.User = context.Users.Find(userId);
                viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);

            }
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
        public ActionResult EditEducationAjax(UserViewModel userViewModel)
        {
            ViewBag.IsCurrentUserPage = true;

            string userId = User.Identity.GetUserId();
            UserEducation oldEducation = context.UserEducations.Find(userViewModel.UserEducation.Id);
            UserEducation userEducation = context.UserEducations.Where(e => e.Id == userViewModel.UserEducation.Id).ToList()[0];

           if (ModelState.IsValid)
            {
                // Education database

                userViewModel.UserEducation.Fk_User = userId;


                Education newEducation = new Education();
                bool educationIsExistedInDB = false;

                for (int i = 0; i < context.Educations.ToList().Count; i++)
                {
                    if (userViewModel.Education.Name == context.Educations.ToList()[i].Name)
                    {
                        educationIsExistedInDB = true;
                    }
                }
                if (educationIsExistedInDB == false)
                {
                    newEducation = userViewModel.Education;
                    context.Educations.Add(newEducation);
                }
                context.SaveChanges();

                // UserEducation database


                oldEducation.Fk_User = userId;
                oldEducation.Activities = userViewModel.UserEducation.Activities;
                oldEducation.Degree = userViewModel.UserEducation.Degree;
                oldEducation.Description = userViewModel.UserEducation.Description;
                oldEducation.FieldOfStudy = userViewModel.UserEducation.FieldOfStudy;
                oldEducation.Grade = userViewModel.UserEducation.Grade;
                oldEducation.StartDate = userViewModel.UserEducation.StartDate;
                oldEducation.EndDate = userViewModel.UserEducation.EndDate;


                if (educationIsExistedInDB == false)
                {
                    oldEducation.Fk_Education = newEducation.Id;

                }
                else
                {
                    oldEducation.Fk_Education = context.Educations.Where(e => e.Name == userViewModel.Education.Name).ToList()[0].Id;
                }
                context.SaveChanges();

                viewModel.User = context.Users.Where(e => e.Id == userEducation.Fk_User).ToArray()[0];


                viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);

            }


            else
            {
                viewModel.User = context.Users.Where(e => e.Id == userEducation.Fk_User).ToArray()[0];


                viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Education_Data", viewModel.User.UserEductions);

            }



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
            ViewBag.IsCurrentUserPage = true;

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
        public ActionResult AddSkillAjax(UserViewModel userViewModel)
        {
            ViewBag.IsCurrentUserPage = true;

            string userId = User.Identity.GetUserId();
            Skill existedSkill = new Skill();

            if (ModelState.IsValid)
            {
                // Skill database
                bool skillIsExistedInDB = false;

                for (int i=0;i<context.Skills.ToList().Count;i++)
                {
                    if (userViewModel.Skill.Name == context.Skills.ToList()[i].Name)
                    {
                        skillIsExistedInDB = true;
                    }
                }
                if (skillIsExistedInDB == false)
                {
                    context.Skills.Add(userViewModel.Skill);
                }
               
                else
                {
                   existedSkill = context.Skills.Where(e => e.Name == userViewModel.Skill.Name).ToList()[0];
                }
                context.SaveChanges();

                // UserSkill database

                UserSkill userSkill = new UserSkill();
                userSkill.Fk_User = userId;
                userSkill.Level= userViewModel.UserSkill.Level;


                if (skillIsExistedInDB == false)
                {
                    userSkill.Fk_Skill = userViewModel.Skill.Id;
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
            {
                viewModel.User = context.Users.Find(userId);
                viewModel.User.UserSkills = context.UserSkills.Include("Skill").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Skill_Data", viewModel.User.UserSkills);
            }

        }


        [HttpGet]
        public ActionResult EditSkillAjax(int id)
        {

            viewModel.UserSkill = context.UserSkills.Include("Skill").Where(e => e.Id == id).ToArray()[0];
            viewModel.UserSkill.Level = context.UserSkills.Include("Skill").Where(e => e.Id == id).ToArray()[0].Level;

            viewModel.Skills = context.Skills.ToList();
            viewModel.Skill = context.Skills.Where(e => e.Id == viewModel.UserSkill.Fk_Skill).ToList()[0];
            viewModel.Skill.Name = context.Skills.Where(e => e.Id == viewModel.UserSkill.Fk_Skill).ToList()[0].Name;
           
            return PartialView("_Partial_Edit_Skill", viewModel);
        }

        [HttpPost]
        public ActionResult EditSkillAjax(UserSkill userSkill)
        {

            ViewBag.IsCurrentUserPage = true;
            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                // Skill database
                
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
                oldSkill.Level = userSkill.Level;

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
                viewModel.User = context.Users.Where(e => e.Id == userSkill.Fk_User).ToArray()[0];

                viewModel.User.UserSkills = context.UserSkills.Include("Skill").Where(e => e.Fk_User == userId).ToList();
                return PartialView("_Partial_Skill_Data", viewModel.User.UserSkills);
            }
            

        }
        [HttpPost]
        public PartialViewResult SearchSkills(string str)
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

            List<Skill> tempSkills = new List<Skill>();
            for (int i = 0; i < wordsNum; i++)
            {
                string tempWord = words[i];
                tempSkills.AddRange(context.Skills.Where(e => e.Name.Contains(tempWord)).ToList());
            }

            List<Skill> skills = new List<Skill>();
            for (int i = 0; i < tempSkills.Count; i++)
            {
                if (!skills.Contains(tempSkills[i]))
                {
                    skills.Add(tempSkills[i]);
                }
            }

            return PartialView("_Partial_Skill_Search", skills);
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
            ViewBag.IsCurrentUserPage = true;
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


        
        public ActionResult UserProfile(string Id)
        {
            viewModel.User = context.Users.Find(Id);
            if (viewModel.User == null)
            {
                // Go to not fount page
            }
            viewModel.User.UserEductions = context.UserEducations.Include("Education").Where(e => e.Fk_User == Id).ToList();
            viewModel.User.UserSkills = context.UserSkills.Include("Skill").Where(e => e.Fk_User == Id).ToList();

            string userId = User.Identity.GetUserId();
            List<Friend> friendship = context.Friends.Where(e => e.Fk_UserOne == userId && e.Fk_UserTwo == Id || e.Fk_UserOne == Id && e.Fk_UserTwo == userId).ToList();
            if (friendship == null || friendship.Count == 0)
            {
                ViewBag.friendState = "noRequest";
            }
            else
            {
                if (friendship[0].isAccepted)
                {
                    ViewBag.friendState = "accepted";
                }
                else
                {
                    ViewBag.friendState = "notAccepted";
                }
            }


            ViewBag.User = context.Users.Find(User.Identity.GetUserId());
            ViewBag.IsCurrentUserPage = false;
            return View("Index",viewModel);
        }

        [HttpPost]
        public void FriendRequest(string Id)
        {
            string userId = User.Identity.GetUserId();
            List<Friend> friendship = context.Friends.Where(e => e.Fk_UserOne == userId && e.Fk_UserTwo == Id || e.Fk_UserOne == Id && e.Fk_UserTwo == userId).ToList();
            if (friendship == null || friendship.Count == 0)
            {
                Friend newOne = new Friend()
                {
                    Fk_UserOne = userId,
                    Fk_UserTwo = Id,
                    isAccepted = false
                };
                context.Friends.Add(newOne);
                context.SaveChanges();
            }
            else
            {
                context.Friends.Remove(friendship[0]);
                context.SaveChanges();
            }

        }
    }
}


