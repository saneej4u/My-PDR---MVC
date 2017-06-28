namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objectives", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Objectives", "Description");
        }
    }
}
