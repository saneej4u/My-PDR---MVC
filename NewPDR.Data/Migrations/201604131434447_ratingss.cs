namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RatingTypeId = c.Int(),
                        ScoreFrom = c.Int(nullable: false),
                        ScoreTo = c.Int(nullable: false),
                        ScoreIndex = c.Int(nullable: false),
                        UserTypeId = c.Int(),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RatingTypes", t => t.RatingTypeId)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId)
                .Index(t => t.RatingTypeId)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.RatingTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Ratings", "RatingTypeId", "dbo.RatingTypes");
            DropIndex("dbo.Ratings", new[] { "UserTypeId" });
            DropIndex("dbo.Ratings", new[] { "RatingTypeId" });
            DropTable("dbo.RatingTypes");
            DropTable("dbo.Ratings");
        }
    }
}
