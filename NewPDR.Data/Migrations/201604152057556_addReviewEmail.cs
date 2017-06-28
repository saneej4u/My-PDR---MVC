namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReviewEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "ReviewerEmailId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "ReviewerEmailId");
        }
    }
}
