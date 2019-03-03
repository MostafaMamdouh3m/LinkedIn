namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatework6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AddColumn("dbo.UserAtWorkplaces", "Industry", c => c.String(nullable: false));
            AddColumn("dbo.UserAtWorkplaces", "Location", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Country_Id");
            DropColumn("dbo.Workplaces", "Industry");
            DropColumn("dbo.Workplaces", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workplaces", "Location", c => c.String());
            AddColumn("dbo.Workplaces", "Industry", c => c.String(nullable: false));
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int(nullable: false));
            DropColumn("dbo.UserAtWorkplaces", "Location");
            DropColumn("dbo.UserAtWorkplaces", "Industry");
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
    }
}
