namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemes2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Fk_Country", "dbo.Countries");
            DropForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations");
            DropIndex("dbo.AspNetUsers", new[] { "Fk_CurrentEducation" });
            DropIndex("dbo.AspNetUsers", new[] { "Fk_Country" });
            AlterColumn("dbo.AspNetUsers", "Fk_CurrentEducation", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Fk_Country", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Fk_CurrentEducation");
            CreateIndex("dbo.AspNetUsers", "Fk_Country");
            AddForeignKey("dbo.AspNetUsers", "Fk_Country", "dbo.Countries", "Id");
            AddForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations");
            DropForeignKey("dbo.AspNetUsers", "Fk_Country", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "Fk_Country" });
            DropIndex("dbo.AspNetUsers", new[] { "Fk_CurrentEducation" });
            AlterColumn("dbo.AspNetUsers", "Fk_Country", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Fk_CurrentEducation", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Fk_Country");
            CreateIndex("dbo.AspNetUsers", "Fk_CurrentEducation");
            AddForeignKey("dbo.AspNetUsers", "Fk_CurrentEducation", "dbo.Educations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Fk_Country", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
