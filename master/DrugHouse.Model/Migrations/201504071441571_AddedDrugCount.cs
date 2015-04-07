namespace DrugHouse.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDrugCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescription", "DrugCount", c => c.Int(nullable: false));
            AddColumn("dbo.Prescription", "Dosage", c => c.String());
            DropColumn("dbo.Prescription", "PrescriptionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prescription", "PrescriptionType", c => c.String());
            DropColumn("dbo.Prescription", "Dosage");
            DropColumn("dbo.Prescription", "DrugCount");
        }
    }
}
