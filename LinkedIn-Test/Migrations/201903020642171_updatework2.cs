namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatework2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AddColumn("dbo.Workplaces", "Industry", c => c.String());
            AddColumn("dbo.Workplaces", "Location", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Country_Id");
            DropColumn("dbo.UserAtWorkplaces", "Company");
            DropColumn("dbo.UserAtWorkplaces", "Location");
            DropColumn("dbo.UserAtWorkplaces", "Industry");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAtWorkplaces", "Industry", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Location", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Company", c => c.String());
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Workplaces", "Location");
            DropColumn("dbo.Workplaces", "Industry");
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
    }
}
