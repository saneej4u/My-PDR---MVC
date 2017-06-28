namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pdp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DevelopmentCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        UserTypeId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.PersonalDevelopmentPlans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DevelopmentNeed = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Solution = c.String(),
                        CostAndResource = c.String(),
                        TimeScale = c.String(),
                        PDReviewId = c.Int(),
                        DevelopmentCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DevelopmentCategories", t => t.DevelopmentCategoryId)
                .ForeignKey("dbo.PDReviews", t => t.PDReviewId)
                .Index(t => t.PDReviewId)
                .Index(t => t.DevelopmentCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalDevelopmentPlans", "PDReviewId", "dbo.PDReviews");
            DropForeignKey("dbo.PersonalDevelopmentPlans", "DevelopmentCategoryId", "dbo.DevelopmentCategories");
            DropForeignKey("dbo.DevelopmentCategories", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.PersonalDevelopmentPlans", new[] { "DevelopmentCategoryId" });
            DropIndex("dbo.PersonalDevelopmentPlans", new[] { "PDReviewId" });
            DropIndex("dbo.DevelopmentCategories", new[] { "UserTypeId" });
            DropTable("dbo.PersonalDevelopmentPlans");
            DropTable("dbo.DevelopmentCategories");
        }
    }
}
