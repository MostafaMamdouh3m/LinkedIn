namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11th : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EducationApplicationUsers", "Education_Id", "dbo.Educations");
            DropForeignKey("dbo.EducationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EducationApplicationUsers", new[] { "Education_Id" });
            DropIndex("dbo.EducationApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "Education_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CurrentEducation_Id", c => c.Int());
            AddColumn("dbo.Educations", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Education_Id");
            CreateIndex("dbo.AspNetUsers", "CurrentEducation_Id");
            CreateIndex("dbo.Educations", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Education_Id", "dbo.Educations", "Id");
            AddForeignKey("dbo.AspNetUsers", "CurrentEducation_Id", "dbo.Educations", "Id");
            AddForeignKey("dbo.Educations", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "CurrentEducation");
            DropTable("dbo.EducationApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EducationApplicationUsers",
                c => new
                    {
                        Education_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Education_Id, t.ApplicationUser_Id });
            
            AddColumn("dbo.AspNetUsers", "CurrentEducation", c => c.String());
            DropForeignKey("dbo.Educations", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CurrentEducation_Id", "dbo.Educations");
            DropForeignKey("dbo.AspNetUsers", "Education_Id", "dbo.Educations");
            DropIndex("dbo.Educations", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CurrentEducation_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Education_Id" });
            DropColumn("dbo.Educations", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "CurrentEducation_Id");
            DropColumn("dbo.AspNetUsers", "Education_Id");
            CreateIndex("dbo.EducationApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.EducationApplicationUsers", "Education_Id");
            AddForeignKey("dbo.EducationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EducationApplicationUsers", "Education_Id", "dbo.Educations", "Id", cascadeDelete: true);
        }
    }
}
