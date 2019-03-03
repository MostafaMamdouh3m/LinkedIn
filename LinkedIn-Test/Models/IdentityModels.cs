using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LinkedIn_Test.Models.Enums;
using LinkedIn_Test.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedIn_Test.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        [MaxLength(50)]
        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string LastName { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual int Age { get; set; }

        public virtual string Headline { get; set; }

        public virtual string ProfilePicture { get; set; }
        public virtual string HeaderPicture { get; set; }
        public virtual string Address { get; set; }
        public virtual string CV { get; set; }

        public virtual string Summary { get; set; }            // add: by mostafa
        public virtual string CurrentPosition { get; set; }    // add: by mostafa


        [ForeignKey("CurrentEducation")]
        public virtual int Fk_CurrentEducation { get; set; }    // add: by mostafa

        public Education CurrentEducation { get; set; }        // add: by mostafa


        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }        // add: by mostafa




        public List<Workplace> Workplaces { get; set; }
        public List<Education> Educations { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Friend> Friends { get; set; }
        public List<Message> Messages { get; set; }
        public List<Post> Posts { get; set; }
 }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<UserAtWorkplace> UserAtWorkplace { get; set; }
        public virtual DbSet<UserHadEducation> UserHadEducation { get; set; }
        public virtual DbSet<UserLikePost> UserLikePost { get; set; }
        public virtual DbSet<Workplace> Workplaces { get; set; }
    }
}