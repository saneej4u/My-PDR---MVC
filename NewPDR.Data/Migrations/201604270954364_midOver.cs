namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class midOver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "MidyearObjectiveOverallRatingId", c => c.Int());
            AddColumn("dbo.PDReviews", "MidYearSuccessFactorOverallRatingId", c => c.Int());
            AddColumn("dbo.PDReviews", "MidYearOverallRatingId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "MidYearOverallRatingId");
            DropColumn("dbo.PDReviews", "MidYearSuccessFactorOverallRatingId");
            DropColumn("dbo.PDReviews", "MidyearObjectiveOverallRatingId");
        }
    }
}
