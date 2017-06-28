namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PDRobjectives : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Objectives",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTimeCreated = c.DateTime(nullable: false),
                        ColleagueComments = c.String(),
                        ManagerComments = c.String(),
                        ColleagueEvidence = c.String(),
                        ManagerEvidence = c.String(),
                        PDReviewsID = c.Int(nullable: false),
                        ObjectiveTypeId = c.Int(nullable: false),
                        PDRReviews_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ObjectiveTypes", t => t.ObjectiveTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PDReviews", t => t.PDRReviews_ID)
                .Index(t => t.ObjectiveTypeId)
                .Index(t => t.PDRReviews_ID);
            
            CreateTable(
                "dbo.ObjectiveTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserTypeId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.PDReviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReviewPeriod = c.DateTime(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        IsMidYear = c.Boolean(nullable: false),
                        IsFullYear = c.Boolean(nullable: false),
                        ColleagueComments = c.String(),
                        ManagerComments = c.String(),
                        ApplicationUserId = c.Int(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PDReviews", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Objectives", "PDRReviews_ID", "dbo.PDReviews");
            DropForeignKey("dbo.PDReviews", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Objectives", "ObjectiveTypeId", "dbo.ObjectiveTypes");
            DropForeignKey("dbo.ObjectiveTypes", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.PDReviews", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PDReviews", new[] { "UserTypeId" });
            DropIndex("dbo.ObjectiveTypes", new[] { "UserTypeId" });
            DropIndex("dbo.Objectives", new[] { "PDRReviews_ID" });
            DropIndex("dbo.Objectives", new[] { "ObjectiveTypeId" });
            DropTable("dbo.PDReviews");
            DropTable("dbo.ObjectiveTypes");
            DropTable("dbo.Objectives");
        }
    }
}
