namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Date = c.DateTime(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        Fk_CommentOwner = c.String(maxLength: 128),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_CommentOwner)
                .Index(t => t.Fk_CommentOwner)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Headline = c.String(),
                        ProfilePicture = c.String(),
                        HeaderPicture = c.String(),
                        Address = c.String(),
                        CV = c.String(),
                        Summary = c.String(),
                        CurrentPosition = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isAccepted = c.Boolean(nullable: false),
                        Fk_UserOne = c.String(maxLength: 128),
                        Fk_UserTwo = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_UserOne)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_UserTwo)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Fk_UserOne)
                .Index(t => t.Fk_UserTwo)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Date = c.DateTime(nullable: false),
                        Fk_Sender = c.String(maxLength: 128),
                        Fk_Reciver = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_Reciver)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_Sender)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Fk_Sender)
                .Index(t => t.Fk_Reciver)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Seen = c.Boolean(nullable: false),
                        Info = c.String(),
                        Date = c.DateTime(nullable: false),
                        Fk_User = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_User)
                .Index(t => t.Fk_User);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Date = c.DateTime(nullable: false),
                        isShared = c.Boolean(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        CommentCount = c.Int(nullable: false),
                        Media = c.String(),
                        Fk_PostOwner = c.String(maxLength: 128),
                        Fk_SharedPost = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_PostOwner)
                .ForeignKey("dbo.Posts", t => t.Fk_SharedPost)
                .Index(t => t.Fk_PostOwner)
                .Index(t => t.Fk_SharedPost);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Workplaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserAtWorkplaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Postion = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CurrentWork = c.Boolean(nullable: false),
                        Fk_User = c.String(maxLength: 128),
                        Fk_Workplace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_User)
                .ForeignKey("dbo.Workplaces", t => t.Fk_Workplace, cascadeDelete: true)
                .Index(t => t.Fk_User)
                .Index(t => t.Fk_Workplace);
            
            CreateTable(
                "dbo.UserHadEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CurrentEducation = c.Boolean(nullable: false),
                        Degree = c.String(),
                        Fk_User = c.String(maxLength: 128),
                        Fk_Education = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Educations", t => t.Fk_Education, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_User)
                .Index(t => t.Fk_User)
                .Index(t => t.Fk_Education);
            
            CreateTable(
                "dbo.UserLikePosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fk_User = c.String(maxLength: 128),
                        Fk_Post = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Fk_Post, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_User)
                .Index(t => t.Fk_User)
                .Index(t => t.Fk_Post);
            
            CreateTable(
                "dbo.EducationApplicationUsers",
                c => new
                    {
                        Education_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Education_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Educations", t => t.Education_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Education_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.WorkplaceApplicationUsers",
                c => new
                    {
                        Workplace_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Workplace_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Workplaces", t => t.Workplace_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Workplace_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLikePosts", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserLikePosts", "Fk_Post", "dbo.Posts");
            DropForeignKey("dbo.UserHadEducations", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserHadEducations", "Fk_Education", "dbo.Educations");
            DropForeignKey("dbo.UserAtWorkplaces", "Fk_Workplace", "dbo.Workplaces");
            DropForeignKey("dbo.UserAtWorkplaces", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Comments", "Fk_CommentOwner", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkplaceApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorkplaceApplicationUsers", "Workplace_Id", "dbo.Workplaces");
            DropForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Fk_SharedPost", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Fk_PostOwner", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Notifications", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Fk_Sender", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Fk_Reciver", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Fk_UserTwo", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Fk_UserOne", "dbo.AspNetUsers");
            DropForeignKey("dbo.EducationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EducationApplicationUsers", "Education_Id", "dbo.Educations");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WorkplaceApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.WorkplaceApplicationUsers", new[] { "Workplace_Id" });
            DropIndex("dbo.EducationApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.EducationApplicationUsers", new[] { "Education_Id" });
            DropIndex("dbo.UserLikePosts", new[] { "Fk_Post" });
            DropIndex("dbo.UserLikePosts", new[] { "Fk_User" });
            DropIndex("dbo.UserHadEducations", new[] { "Fk_Education" });
            DropIndex("dbo.UserHadEducations", new[] { "Fk_User" });
            DropIndex("dbo.UserAtWorkplaces", new[] { "Fk_Workplace" });
            DropIndex("dbo.UserAtWorkplaces", new[] { "Fk_User" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Skills", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Posts", new[] { "Fk_SharedPost" });
            DropIndex("dbo.Posts", new[] { "Fk_PostOwner" });
            DropIndex("dbo.Notifications", new[] { "Fk_User" });
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Messages", new[] { "Fk_Reciver" });
            DropIndex("dbo.Messages", new[] { "Fk_Sender" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Friends", new[] { "Fk_UserTwo" });
            DropIndex("dbo.Friends", new[] { "Fk_UserOne" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "Fk_CommentOwner" });
            DropTable("dbo.WorkplaceApplicationUsers");
            DropTable("dbo.EducationApplicationUsers");
            DropTable("dbo.UserLikePosts");
            DropTable("dbo.UserHadEducations");
            DropTable("dbo.UserAtWorkplaces");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Countries");
            DropTable("dbo.Workplaces");
            DropTable("dbo.Skills");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Posts");
            DropTable("dbo.Notifications");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Friends");
            DropTable("dbo.Educations");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
