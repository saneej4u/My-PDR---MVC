namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HRProPersonnelRecords : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HRProPersonnelRecords");
        }
    }
}
