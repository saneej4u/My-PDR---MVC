namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5newPDRDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserType_ID", "dbo.UserTypes");
            DropIndex("dbo.AspNetUsers", new[] { "UserType_ID" });
            AddColumn("dbo.AspNetUsers", "UserTypeID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserType_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserType_ID", c => c.Int());
            DropColumn("dbo.AspNetUsers", "UserTypeID");
            CreateIndex("dbo.AspNetUsers", "UserType_ID");
            AddForeignKey("dbo.AspNetUsers", "UserType_ID", "dbo.UserTypes", "ID");
        }
    }
}
