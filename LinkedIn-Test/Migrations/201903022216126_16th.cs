namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16th : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations");
            DropIndex("dbo.AspNetUsers", new[] { "Fk_CurrentEducation" });
            AlterColumn("dbo.AspNetUsers", "Fk_CurrentEducation", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Fk_CurrentEducation");
            AddForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations");
            DropIndex("dbo.AspNetUsers", new[] { "Fk_CurrentEducation" });
            AlterColumn("dbo.AspNetUsers", "Fk_CurrentEducation", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Fk_CurrentEducation");
            AddForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations", "Id");
        }
    }
}
