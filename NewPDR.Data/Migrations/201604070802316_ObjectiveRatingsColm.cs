namespace NewPDR.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObjectiveRatingsColm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objectives", "MidYearRating", c => c.Int(nullable: false));
            AddColumn("dbo.Objectives", "FullYearRating", c => c.Int(nullable: false));
            AddColumn("dbo.ObjectiveTypes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ObjectiveTypes", "Description");
            DropColumn("dbo.Objectives", "FullYearRating");
            DropColumn("dbo.Objectives", "MidYearRating");
        }
    }
}
