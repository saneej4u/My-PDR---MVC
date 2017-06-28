namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removePDRStatusColm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PDReviews", "PDRStatus_ID", "dbo.PDRStatus");
            DropIndex("dbo.PDReviews", new[] { "PDRStatus_ID" });
            DropColumn("dbo.PDReviews", "PDRStatus_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PDReviews", "PDRStatus_ID", c => c.Int());
            CreateIndex("dbo.PDReviews", "PDRStatus_ID");
            AddForeignKey("dbo.PDReviews", "PDRStatus_ID", "dbo.PDRStatus", "ID");
        }
    }
}
