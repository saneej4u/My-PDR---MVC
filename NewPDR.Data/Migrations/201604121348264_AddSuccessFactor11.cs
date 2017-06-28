namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuccessFactor11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SuccessFactors", "PDReviewId", "dbo.PDReviews");
            DropIndex("dbo.SuccessFactors", new[] { "PDReviewId" });
            AlterColumn("dbo.Objectives", "PDReviewId", c => c.Int());
            AlterColumn("dbo.SuccessFactors", "PDReviewId", c => c.Int());
            CreateIndex("dbo.Objectives", "PDReviewId");
            CreateIndex("dbo.SuccessFactors", "PDReviewId");
            AddForeignKey("dbo.Objectives", "PDReviewId", "dbo.PDReviews", "ID");
            AddForeignKey("dbo.SuccessFactors", "PDReviewId", "dbo.PDReviews", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuccessFactors", "PDReviewId", "dbo.PDReviews");
            DropForeignKey("dbo.Objectives", "PDReviewId", "dbo.PDReviews");
            DropIndex("dbo.SuccessFactors", new[] { "PDReviewId" });
            DropIndex("dbo.Objectives", new[] { "PDReviewId" });
            AlterColumn("dbo.SuccessFactors", "PDReviewId", c => c.Int(nullable: false));
            AlterColumn("dbo.Objectives", "PDReviewId", c => c.Int(nullable: false));
            CreateIndex("dbo.SuccessFactors", "PDReviewId");
            AddForeignKey("dbo.SuccessFactors", "PDReviewId", "dbo.PDReviews", "ID", cascadeDelete: true);
        }
    }
}
