namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTimeCreated = c.DateTime(nullable: false),
                        Task = c.String(),
                        Status = c.String(),
                        ApplicationUserId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoLists", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoLists", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ToDoLists");
        }
    }
}
