namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExperience : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AddColumn("dbo.Workplaces", "Company", c => c.String(nullable: false));
            AddColumn("dbo.Workplaces", "Location", c => c.String());
            AddColumn("dbo.Workplaces", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Workplaces", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Workplaces", "HeadLine", c => c.String(nullable: false));
            AddColumn("dbo.Workplaces", "Description", c => c.String());
            AddColumn("dbo.Workplaces", "Industry", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Workplaces", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.Workplaces", "Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Workplaces", "Industry");
            DropColumn("dbo.Workplaces", "Description");
            DropColumn("dbo.Workplaces", "HeadLine");
            DropColumn("dbo.Workplaces", "EndDate");
            DropColumn("dbo.Workplaces", "StartDate");
            DropColumn("dbo.Workplaces", "Location");
            DropColumn("dbo.Workplaces", "Company");
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
    }
}
