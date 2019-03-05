namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageAdds : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Messages", newName: "Message");
            AddColumn("dbo.AspNetUsers", "MessageUpdated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "Recived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "Seen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "Seen");
            DropColumn("dbo.Message", "Recived");
            DropColumn("dbo.AspNetUsers", "MessageUpdated");
            RenameTable(name: "dbo.Message", newName: "Messages");
        }
    }
}
