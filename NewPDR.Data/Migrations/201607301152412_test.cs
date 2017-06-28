namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Name = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfilePicUrl = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        LastLoginTime = c.DateTime(),
                        Activated = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PDReviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReviewerUserId = c.String(),
                        ReviewerEmailId = c.String(),
                        ReviewPeriod = c.DateTime(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        MidYearStatus = c.Int(nullable: false),
                        FullYearStatus = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        LockedAt = c.DateTime(),
                        LockEndTime = c.DateTime(),
                        LockedBy = c.String(),
                        IsLocked = c.Boolean(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                        ObjectiveOverallRatingId = c.Int(),
                        SuccessFactorOverallRatingId = c.Int(),
                        OverallRatingId = c.Int(),
                        IsMidYear = c.Boolean(nullable: false),
                        IsFullYear = c.Boolean(nullable: false),
                        MidyearObjectiveOverallScore = c.Int(),
                        MidYearSuccessFactorOverallScore = c.Int(),
                        MidYearOverallScore = c.Int(),
                        FullYearObjectiveOverallScore = c.Int(),
                        FullYearSuccessFactorOverallScore = c.Int(),
                        FullYearOverallScore = c.Int(),
                        ColleagueOverallComments = c.String(),
                        ColleagueSigned = c.String(),
                        ColleagueSignedDate = c.DateTime(),
                        ManagerOverallComments = c.String(),
                        ManagerSigned = c.String(),
                        ManagerSignedDate = c.DateTime(),
                        AccessRequest = c.String(),
                        AccessResponse = c.String(),
                        AcessRequestedUserId = c.String(),
                        LineManagerEmailId = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.UserTypeId);
            
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
                        MidYearRating = c.Int(nullable: false),
                        FullYearRating = c.Int(nullable: false),
                        Description = c.String(),
                        PDReviewId = c.Int(),
                        ObjectiveTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ObjectiveTypes", t => t.ObjectiveTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PDReviews", t => t.PDReviewId)
                .Index(t => t.PDReviewId)
                .Index(t => t.ObjectiveTypeId);
            
            CreateTable(
                "dbo.ObjectiveTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserTypeId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        PDReviewId = c.Int(),
                        SuccessFactorTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PDReviews", t => t.PDReviewId)
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ToDoLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Task = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
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
                "dbo.HRProPersonnelRecords",
                c => new
                    {
                        EmailId = c.String(nullable: false, maxLength: 128),
                        DateTimeCreated = c.DateTime(nullable: false),
                        JobTitle = c.String(),
                        ForeName = c.String(),
                        Surname = c.String(),
                        Department = c.String(),
                        Division = c.String(),
                        ManagerEmailId = c.String(),
                        LineManagerEmailId = c.String(),
                    })
                .PrimaryKey(t => t.EmailId);
            
            CreateTable(
                "dbo.PDRStatus",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Descriptions = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        DevelopmentCatId = c.Int(nullable: false),
                        DevelopmentCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DevelopmentCategories", t => t.DevelopmentCategory_ID)
                .ForeignKey("dbo.PDReviews", t => t.PDReviewId)
                .Index(t => t.PDReviewId)
                .Index(t => t.DevelopmentCategory_ID);
            
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
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ratings", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Ratings", "RatingTypeId", "dbo.RatingTypes");
            DropForeignKey("dbo.PersonalDevelopmentPlans", "PDReviewId", "dbo.PDReviews");
            DropForeignKey("dbo.PersonalDevelopmentPlans", "DevelopmentCategory_ID", "dbo.DevelopmentCategories");
            DropForeignKey("dbo.DevelopmentCategories", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Activities", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.ToDoLists", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PDReviews", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.SuccessFactors", "SuccessFactorTypeId", "dbo.SuccessFactorTypes");
            DropForeignKey("dbo.SuccessFactorTypes", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.SuccessFactors", "PDReviewId", "dbo.PDReviews");
            DropForeignKey("dbo.Objectives", "PDReviewId", "dbo.PDReviews");
            DropForeignKey("dbo.Objectives", "ObjectiveTypeId", "dbo.ObjectiveTypes");
            DropForeignKey("dbo.ObjectiveTypes", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.PDReviews", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ratings", new[] { "UserTypeId" });
            DropIndex("dbo.Ratings", new[] { "RatingTypeId" });
            DropIndex("dbo.PersonalDevelopmentPlans", new[] { "DevelopmentCategory_ID" });
            DropIndex("dbo.PersonalDevelopmentPlans", new[] { "PDReviewId" });
            DropIndex("dbo.DevelopmentCategories", new[] { "UserTypeId" });
            DropIndex("dbo.ToDoLists", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.SuccessFactorTypes", new[] { "UserTypeId" });
            DropIndex("dbo.SuccessFactors", new[] { "SuccessFactorTypeId" });
            DropIndex("dbo.SuccessFactors", new[] { "PDReviewId" });
            DropIndex("dbo.ObjectiveTypes", new[] { "UserTypeId" });
            DropIndex("dbo.Objectives", new[] { "ObjectiveTypeId" });
            DropIndex("dbo.Objectives", new[] { "PDReviewId" });
            DropIndex("dbo.PDReviews", new[] { "UserTypeId" });
            DropIndex("dbo.PDReviews", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "UserTypeId" });
            DropIndex("dbo.Activities", new[] { "ApplicationUserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RatingTypes");
            DropTable("dbo.Ratings");
            DropTable("dbo.PersonalDevelopmentPlans");
            DropTable("dbo.PDRStatus");
            DropTable("dbo.HRProPersonnelRecords");
            DropTable("dbo.DevelopmentCategories");
            DropTable("dbo.ToDoLists");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.SuccessFactorTypes");
            DropTable("dbo.SuccessFactors");
            DropTable("dbo.UserTypes");
            DropTable("dbo.ObjectiveTypes");
            DropTable("dbo.Objectives");
            DropTable("dbo.PDReviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Activities");
        }
    }
}
