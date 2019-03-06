namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial222 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Skills", new[] { "ApplicationUser_Id" });
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
            
            AddColumn("dbo.AspNetUsers", "Skill_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Skill_Id");
            AddForeignKey("dbo.AspNetUsers", "Skill_Id", "dbo.Skills", "Id");
            DropColumn("dbo.Skills", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.UserSkill", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSkill", "Fk_Skill", "dbo.Skills");
            DropForeignKey("dbo.AspNetUsers", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.UserSkill", new[] { "Fk_Skill" });
            DropIndex("dbo.UserSkill", new[] { "Fk_User" });
            DropIndex("dbo.AspNetUsers", new[] { "Skill_Id" });
            DropColumn("dbo.AspNetUsers", "Skill_Id");
            DropTable("dbo.UserSkill");
            CreateIndex("dbo.Skills", "ApplicationUser_Id");
            AddForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
