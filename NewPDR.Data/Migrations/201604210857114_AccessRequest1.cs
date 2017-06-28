namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccessRequest1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PDReviews", "AcessRequestedUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PDReviews", "AcessRequestedUserId", c => c.Int());
        }
    }
}
