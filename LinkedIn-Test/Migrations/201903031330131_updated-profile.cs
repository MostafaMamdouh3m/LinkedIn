namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAtWorkplaces", "HeadLine", c => c.String(nullable: false));
            AddColumn("dbo.UserAtWorkplaces", "Title", c => c.String(nullable: false));
            AddColumn("dbo.UserAtWorkplaces", "Description", c => c.String());
            AddColumn("dbo.UserAtWorkplaces", "Industry", c => c.String(nullable: false));
            AddColumn("dbo.UserAtWorkplaces", "Location", c => c.String());
            AlterColumn("dbo.Workplaces", "Name", c => c.String(nullable: false));
            DropColumn("dbo.UserAtWorkplaces", "Postion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAtWorkplaces", "Postion", c => c.String());
            AlterColumn("dbo.Workplaces", "Name", c => c.String());
            DropColumn("dbo.UserAtWorkplaces", "Location");
            DropColumn("dbo.UserAtWorkplaces", "Industry");
            DropColumn("dbo.UserAtWorkplaces", "Description");
            DropColumn("dbo.UserAtWorkplaces", "Title");
            DropColumn("dbo.UserAtWorkplaces", "HeadLine");
        }
    }
}
