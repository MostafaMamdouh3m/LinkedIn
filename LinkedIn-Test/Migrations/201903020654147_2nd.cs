namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2nd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            AlterColumn("dbo.AspNetUsers", "Country_id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Country_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Country_Id");
        }
    }
}
