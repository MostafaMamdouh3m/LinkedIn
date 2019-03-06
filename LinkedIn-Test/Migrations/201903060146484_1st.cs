namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1st : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSkill", "Fk_Skill", "dbo.Skills");
            DropForeignKey("dbo.UserSkill", "Fk_User", "dbo.AspNetUsers");
            DropIndex("dbo.Skills", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserSkill", new[] { "Fk_User" });
            DropIndex("dbo.UserSkill", new[] { "Fk_Skill" });
            AddColumn("dbo.AspNetUsers", "Skill_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Skill_Id");
            AddForeignKey("dbo.AspNetUsers", "Skill_Id", "dbo.Skills", "Id");
            DropColumn("dbo.Skills", "ApplicationUser_Id");
            DropTable("dbo.UserSkill");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserSkill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoOfSkilledUsers = c.Int(nullable: false),
                        Fk_User = c.String(maxLength: 128),
                        Fk_Skill = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Skills", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.AspNetUsers", new[] { "Skill_Id" });
            DropColumn("dbo.AspNetUsers", "Skill_Id");
            CreateIndex("dbo.UserSkill", "Fk_Skill");
            CreateIndex("dbo.UserSkill", "Fk_User");
            CreateIndex("dbo.Skills", "ApplicationUser_Id");
            AddForeignKey("dbo.UserSkill", "Fk_User", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserSkill", "Fk_Skill", "dbo.Skills", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
