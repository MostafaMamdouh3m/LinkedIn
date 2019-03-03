namespace LinkedIn_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15th : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "CurrentEducation_Id", newName: "Fk_CurrentEducation");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_CurrentEducation_Id", newName: "IX_Fk_CurrentEducation");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Fk_CurrentEducation", newName: "IX_CurrentEducation_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Fk_CurrentEducation", newName: "CurrentEducation_Id");
        }
    }
}
