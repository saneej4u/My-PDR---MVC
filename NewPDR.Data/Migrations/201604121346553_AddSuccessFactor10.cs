namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuccessFactor10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuccessFactors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        ColleagueComments = c.String(),
                        ManagerComments = c.String(),
                        Strengths = c.String(),
                        Improvements = c.String(),
                        MidYearRating = c.Int(nullable: false),
                        FullYearRating = c.Int(nullable: false),
                        PDReviewId = c.Int(nullable: false),
                        SuccessFactorTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PDReviews", t => t.PDReviewId, cascadeDelete: true)
                .ForeignKey("dbo.SuccessFactorTypes", t => t.SuccessFactorTypeId, cascadeDelete: true)
                .Index(t => t.PDReviewId)
                .Index(t => t.SuccessFactorTypeId);
            
            CreateTable(
                "dbo.SuccessFactorTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Index = c.Int(nullable: false),
                        UserTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId)
                .Index(t => t.UserTypeId);
            
            AddColumn("dbo.ObjectiveTypes", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuccessFactors", "SuccessFactorTypeId", "dbo.SuccessFactorTypes");
            DropForeignKey("dbo.SuccessFactorTypes", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.SuccessFactors", "PDReviewId", "dbo.PDReviews");
            DropIndex("dbo.SuccessFactorTypes", new[] { "UserTypeId" });
            DropIndex("dbo.SuccessFactors", new[] { "SuccessFactorTypeId" });
            DropIndex("dbo.SuccessFactors", new[] { "PDReviewId" });
            DropColumn("dbo.ObjectiveTypes", "Index");
            DropTable("dbo.SuccessFactorTypes");
            DropTable("dbo.SuccessFactors");
        }
    }
}
