namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemessi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "FK_postId");
            AlterColumn("dbo.Comments", "FK_postId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "FK_postId");
            AddForeignKey("dbo.Comments", "FK_postId", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "FK_postId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "FK_postId" });
            AlterColumn("dbo.Comments", "FK_postId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "FK_postId", newName: "Post_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
