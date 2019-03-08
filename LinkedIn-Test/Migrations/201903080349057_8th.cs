namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8th : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserSkill", "Level", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSkill", "Level", c => c.Int(nullable: false));
        }
    }
}
