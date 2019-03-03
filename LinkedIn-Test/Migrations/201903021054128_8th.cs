namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8th : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AddColumn("dbo.UserHadEducations", "FieldOfStudy", c => c.String());
            AddColumn("dbo.UserHadEducations", "Grade", c => c.Int(nullable: false));
            AddColumn("dbo.UserHadEducations", "Activities", c => c.String());
            AddColumn("dbo.UserHadEducations", "Description", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Headline", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Educations", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.Educations", "Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Headline", c => c.String());
            DropColumn("dbo.UserHadEducations", "Description");
            DropColumn("dbo.UserHadEducations", "Activities");
            DropColumn("dbo.UserHadEducations", "Grade");
            DropColumn("dbo.UserHadEducations", "FieldOfStudy");
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
    }
}
