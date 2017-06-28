namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3newPDRDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserType_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserType_ID");
            AddForeignKey("dbo.AspNetUsers", "UserType_ID", "dbo.UserTypes", "ID");
            DropColumn("dbo.AspNetUsers", "UserTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "UserType_ID", "dbo.UserTypes");
            DropIndex("dbo.AspNetUsers", new[] { "UserType_ID" });
            DropColumn("dbo.AspNetUsers", "UserType_ID");
        }
    }
}
