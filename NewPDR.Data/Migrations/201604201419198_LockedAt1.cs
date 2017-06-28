namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LockedAt1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PDReviews", "LockedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PDReviews", "LockedAt", c => c.DateTime(nullable: false));
        }
    }
}
