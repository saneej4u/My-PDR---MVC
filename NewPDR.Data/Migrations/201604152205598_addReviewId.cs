namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReviewId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "ReviewerUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "ReviewerUserId");
        }
    }
}
