namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteComments : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PDReviews", "ColleagueComments");
            DropColumn("dbo.PDReviews", "ManagerComments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PDReviews", "ManagerComments", c => c.String());
            AddColumn("dbo.PDReviews", "ColleagueComments", c => c.String());
        }
    }
}
