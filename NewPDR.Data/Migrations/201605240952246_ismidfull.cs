namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ismidfull : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDReviews", "IsMidYear", c => c.Boolean(nullable: false));
            AddColumn("dbo.PDReviews", "IsFullYear", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDReviews", "IsFullYear");
            DropColumn("dbo.PDReviews", "IsMidYear");
        }
    }
}
