namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class devcatid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PersonalDevelopmentPlans", name: "DevelopmentCategoryId", newName: "DevelopmentCategory_ID");
            RenameIndex(table: "dbo.PersonalDevelopmentPlans", name: "IX_DevelopmentCategoryId", newName: "IX_DevelopmentCategory_ID");
            AddColumn("dbo.PersonalDevelopmentPlans", "DevelopmentCatId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonalDevelopmentPlans", "DevelopmentCatId");
            RenameIndex(table: "dbo.PersonalDevelopmentPlans", name: "IX_DevelopmentCategory_ID", newName: "IX_DevelopmentCategoryId");
            RenameColumn(table: "dbo.PersonalDevelopmentPlans", name: "DevelopmentCategory_ID", newName: "DevelopmentCategoryId");
        }
    }
}
