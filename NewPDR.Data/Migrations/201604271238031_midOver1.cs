namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class midOver1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "MidyearObjectiveOverallScore", c => c.Int());
            AddColumn("dbo.PDReviews", "MidYearSuccessFactorOverallScore", c => c.Int());
            AddColumn("dbo.PDReviews", "MidYearOverallScore", c => c.Int());
            AddColumn("dbo.PDReviews", "FullYearObjectiveOverallScore", c => c.Int());
            AddColumn("dbo.PDReviews", "FullYearSuccessFactorOverallScore", c => c.Int());
            AddColumn("dbo.PDReviews", "FullYearOverallScore", c => c.Int());
            DropColumn("dbo.PDReviews", "MidyearObjectiveOverallRatingId");
            DropColumn("dbo.PDReviews", "MidYearSuccessFactorOverallRatingId");
            DropColumn("dbo.PDReviews", "MidYearOverallRatingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PDReviews", "MidYearOverallRatingId", c => c.Int());
            AddColumn("dbo.PDReviews", "MidYearSuccessFactorOverallRatingId", c => c.Int());
            AddColumn("dbo.PDReviews", "MidyearObjectiveOverallRatingId", c => c.Int());
            DropColumn("dbo.PDReviews", "FullYearOverallScore");
            DropColumn("dbo.PDReviews", "FullYearSuccessFactorOverallScore");
            DropColumn("dbo.PDReviews", "FullYearObjectiveOverallScore");
            DropColumn("dbo.PDReviews", "MidYearOverallScore");
            DropColumn("dbo.PDReviews", "MidYearSuccessFactorOverallScore");
            DropColumn("dbo.PDReviews", "MidyearObjectiveOverallScore");
        }
    }
}
