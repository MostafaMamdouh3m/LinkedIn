namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSkill", "MyProperty", c => c.Int(nullable: false));
            AddColumn("dbo.UserSkill", "Level", c => c.Int(nullable: false));
            DropColumn("dbo.Skills", "Level");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Level", c => c.Int(nullable: false));
            DropColumn("dbo.UserSkill", "Level");
            DropColumn("dbo.UserSkill", "MyProperty");
        }
    }
}
