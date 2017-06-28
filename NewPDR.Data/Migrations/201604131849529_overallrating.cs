namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class overallrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "ObjectiveOverallRatingId", c => c.Int());
            AddColumn("dbo.PDReviews", "SuccessFactorOverallRatingId", c => c.Int());
            AddColumn("dbo.PDReviews", "OverallRatingId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "OverallRatingId");
            DropColumn("dbo.PDReviews", "SuccessFactorOverallRatingId");
            DropColumn("dbo.PDReviews", "ObjectiveOverallRatingId");
        }
    }
}
