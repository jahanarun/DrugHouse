namespace DrugHouse.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedDrugCountToDosage : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Prescription", "DrugCount", "Duration");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Prescription", "Duration", "DrugCount");
        }
    }
}
