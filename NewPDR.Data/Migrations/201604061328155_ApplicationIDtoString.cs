namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationIDtoString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PDReviews", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.PDReviews", "ApplicationUserId");
            RenameColumn(table: "dbo.PDReviews", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.PDReviews", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PDReviews", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PDReviews", new[] { "ApplicationUserId" });
            AlterColumn("dbo.PDReviews", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PDReviews", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.PDReviews", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.PDReviews", "ApplicationUser_Id");
        }
    }
}
