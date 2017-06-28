namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoLists", "IsCompleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.ToDoLists", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoLists", "Status", c => c.String());
            DropColumn("dbo.ToDoLists", "IsCompleted");
        }
    }
}
