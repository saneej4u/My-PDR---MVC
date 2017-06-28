namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TodoList123 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ToDoLists", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ToDoLists", "ApplicationUserId");
            RenameColumn(table: "dbo.ToDoLists", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.ToDoLists", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ToDoLists", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ToDoLists", new[] { "ApplicationUserId" });
            AlterColumn("dbo.ToDoLists", "ApplicationUserId", c => c.Int());
            RenameColumn(table: "dbo.ToDoLists", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.ToDoLists", "ApplicationUserId", c => c.Int());
            CreateIndex("dbo.ToDoLists", "ApplicationUser_Id");
        }
    }
}
