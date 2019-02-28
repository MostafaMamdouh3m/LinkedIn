namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLogin : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "Fk_SharedPost" });
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Posts", "Fk_SharedPost", c => c.Int());
            CreateIndex("dbo.Posts", "Fk_SharedPost");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "Fk_SharedPost" });
            AlterColumn("dbo.Posts", "Fk_SharedPost", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            CreateIndex("dbo.Posts", "Fk_SharedPost");
        }
    }
}
