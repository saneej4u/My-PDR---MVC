namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PDRStatusColm1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PDRStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PDReviews", "MidYearStatus", c => c.Int(nullable: false));
            AddColumn("dbo.PDReviews", "FullYearStatus", c => c.Int(nullable: false));
            AddColumn("dbo.PDReviews", "LastModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.PDReviews", "LastModifiedBy", c => c.String());
            AddColumn("dbo.PDReviews", "PDRStatus_ID", c => c.Int());
            CreateIndex("dbo.PDReviews", "PDRStatus_ID");
            AddForeignKey("dbo.PDReviews", "PDRStatus_ID", "dbo.PDRStatus", "ID");
            DropColumn("dbo.PDReviews", "IsMidYear");
            DropColumn("dbo.PDReviews", "IsFullYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PDReviews", "IsFullYear", c => c.Boolean(nullable: false));
            AddColumn("dbo.PDReviews", "IsMidYear", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PDReviews", "PDRStatus_ID", "dbo.PDRStatus");
            DropIndex("dbo.PDReviews", new[] { "PDRStatus_ID" });
            DropColumn("dbo.PDReviews", "PDRStatus_ID");
            DropColumn("dbo.PDReviews", "LastModifiedBy");
            DropColumn("dbo.PDReviews", "LastModifiedOn");
            DropColumn("dbo.PDReviews", "FullYearStatus");
            DropColumn("dbo.PDReviews", "MidYearStatus");
            DropTable("dbo.PDRStatus");
        }
    }
}
