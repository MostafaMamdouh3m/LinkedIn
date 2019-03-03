namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatework : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AddColumn("dbo.UserAtWorkplaces", "HeadLine", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Company", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Location", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Description", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Industry", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Workplaces", "Name", c => c.String());
            CreateIndex("dbo.AspNetUsers", "Country_Id");
            DropColumn("dbo.Workplaces", "Company");
            DropColumn("dbo.Workplaces", "Location");
            DropColumn("dbo.Workplaces", "StartDate");
            DropColumn("dbo.Workplaces", "EndDate");
            DropColumn("dbo.Workplaces", "HeadLine");
            DropColumn("dbo.Workplaces", "Description");
            DropColumn("dbo.Workplaces", "Industry");
            DropColumn("dbo.UserAtWorkplaces", "Postion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAtWorkplaces", "Postion", c => c.String());
            AddColumn("dbo.Workplaces", "Industry", c => c.String());
            AddColumn("dbo.Workplaces", "Description", c => c.String());
            AddColumn("dbo.Workplaces", "HeadLine", c => c.String(nullable: false));
            AddColumn("dbo.Workplaces", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Workplaces", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Workplaces", "Location", c => c.String());
            AddColumn("dbo.Workplaces", "Company", c => c.String(nullable: false));
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.Workplaces", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int(nullable: false));
            DropColumn("dbo.UserAtWorkplaces", "Industry");
            DropColumn("dbo.UserAtWorkplaces", "Description");
            DropColumn("dbo.UserAtWorkplaces", "Location");
            DropColumn("dbo.UserAtWorkplaces", "Company");
            DropColumn("dbo.UserAtWorkplaces", "HeadLine");
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
    }
}
