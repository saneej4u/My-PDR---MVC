namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14newPDRDB : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "UserTypeId");
            AddForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.AspNetUsers", new[] { "UserTypeId" });
        }
    }
}
