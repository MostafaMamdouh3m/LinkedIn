namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserEducation", "Grade", c => c.Int());
            AlterColumn("dbo.UserSkill", "Level", c => c.Int());
            AlterColumn("dbo.Skills", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skills", "Name", c => c.String());
            AlterColumn("dbo.UserSkill", "Level", c => c.Int(nullable: false));
            AlterColumn("dbo.UserEducation", "Grade", c => c.Int(nullable: false));
        }
    }
}
