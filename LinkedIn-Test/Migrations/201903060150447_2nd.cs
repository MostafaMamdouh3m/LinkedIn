namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2nd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSkill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fk_User = c.String(maxLength: 128),
                        Fk_Skill = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.Fk_Skill, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Fk_User)
                .Index(t => t.Fk_User)
                .Index(t => t.Fk_Skill);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSkill", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSkill", "Fk_Skill", "dbo.Skills");
            DropIndex("dbo.UserSkill", new[] { "Fk_Skill" });
            DropIndex("dbo.UserSkill", new[] { "Fk_User" });
            DropTable("dbo.UserSkill");
        }
    }
}
