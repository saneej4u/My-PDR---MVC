namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationIDtoString4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Objectives", name: "PDRReviews_ID", newName: "PDReview_ID");
            RenameIndex(table: "dbo.Objectives", name: "IX_PDRReviews_ID", newName: "IX_PDReview_ID");
            DropColumn("dbo.Objectives", "PDReviewsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objectives", "PDReviewsID", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Objectives", name: "IX_PDReview_ID", newName: "IX_PDRReviews_ID");
            RenameColumn(table: "dbo.Objectives", name: "PDReview_ID", newName: "PDRReviews_ID");
        }
    }
}
