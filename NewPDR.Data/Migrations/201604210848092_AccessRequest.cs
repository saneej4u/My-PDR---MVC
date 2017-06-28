namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccessRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "AccessRequest", c => c.String());
            AddColumn("dbo.PDReviews", "AccessResponse", c => c.String());
            AddColumn("dbo.PDReviews", "AcessRequestedUserId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "AcessRequestedUserId");
            DropColumn("dbo.PDReviews", "AccessResponse");
            DropColumn("dbo.PDReviews", "AccessRequest");
        }
    }
}
