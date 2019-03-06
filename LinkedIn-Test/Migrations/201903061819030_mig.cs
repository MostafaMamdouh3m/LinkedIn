namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Skills", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "FK_postId");
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
            AlterColumn("dbo.Comments", "FK_postId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "FK_postId");
            CreateIndex("dbo.AspNetUsers", "Skill_Id");
            AddForeignKey("dbo.AspNetUsers", "Skill_Id", "dbo.Skills", "Id");
            AddForeignKey("dbo.Comments", "FK_postId", "dbo.Posts", "Id", cascadeDelete: true);
            DropColumn("dbo.Skills", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Comments", "FK_postId", "dbo.Posts");
            DropForeignKey("dbo.UserSkill", "Fk_User", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSkill", "Fk_Skill", "dbo.Skills");
            DropForeignKey("dbo.AspNetUsers", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.UserSkill", new[] { "Fk_Skill" });
            DropIndex("dbo.UserSkill", new[] { "Fk_User" });
            DropIndex("dbo.AspNetUsers", new[] { "Skill_Id" });
            DropIndex("dbo.Comments", new[] { "FK_postId" });
            AlterColumn("dbo.Comments", "FK_postId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Skill_Id");
            DropTable("dbo.UserSkill");
            RenameColumn(table: "dbo.Comments", name: "FK_postId", newName: "Post_Id");
            CreateIndex("dbo.Skills", "ApplicationUser_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Skills", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
