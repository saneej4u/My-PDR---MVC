namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LockedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "LockedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PDReviews", "LockedBy", c => c.String());
            AddColumn("dbo.PDReviews", "IsLocked", c => c.Boolean(nullable: false));
            DropColumn("dbo.PDReviews", "LastModifiedOn");
            DropColumn("dbo.PDReviews", "LastModifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PDReviews", "LastModifiedBy", c => c.String());
            AddColumn("dbo.PDReviews", "LastModifiedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.PDReviews", "IsLocked");
            DropColumn("dbo.PDReviews", "LockedBy");
            DropColumn("dbo.PDReviews", "LockedAt");
        }
    }
}
