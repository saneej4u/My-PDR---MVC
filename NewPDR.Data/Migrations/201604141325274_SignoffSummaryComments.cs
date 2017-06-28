namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SignoffSummaryComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "ColleagueOverallComments", c => c.String());
            AddColumn("dbo.PDReviews", "ColleagueSigned", c => c.String());
            AddColumn("dbo.PDReviews", "ColleagueSignedDate", c => c.DateTime());
            AddColumn("dbo.PDReviews", "ManagerOverallComments", c => c.String());
            AddColumn("dbo.PDReviews", "ManagerSigned", c => c.String());
            AddColumn("dbo.PDReviews", "ManagerSignedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "ManagerSignedDate");
            DropColumn("dbo.PDReviews", "ManagerSigned");
            DropColumn("dbo.PDReviews", "ManagerOverallComments");
            DropColumn("dbo.PDReviews", "ColleagueSignedDate");
            DropColumn("dbo.PDReviews", "ColleagueSigned");
            DropColumn("dbo.PDReviews", "ColleagueOverallComments");
        }
    }
}
