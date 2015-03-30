namespace DrugHouse.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drug",
                c => new
                    {
                        DrugId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DrugType = c.Int(nullable: false),
                        Remarks = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.DrugId);
            
            CreateTable(
                "dbo.MedicalPractitioner",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EducationDegree = c.String(),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        GuardianName = c.String(),
                        GuardianRelationship = c.Int(nullable: false),
                        Remark = c.String(),
                        Location_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimpleEntity", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.SimpleEntity",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Marker = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientAdmitance",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DoctorFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Patient_Id = c.Long(),
                        PrimaryDiagnosis_Id = c.Long(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patient", t => t.Patient_Id)
                .ForeignKey("dbo.SimpleEntity", t => t.PrimaryDiagnosis_Id)
                .ForeignKey("dbo.PatientStatus", t => t.Status_Id)
                .Index(t => t.Patient_Id)
                .Index(t => t.PrimaryDiagnosis_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        GuardianName = c.String(),
                        GuardianRelationship = c.Int(nullable: false),
                        Remark = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Location_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimpleEntity", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.PatientVisit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Complaints = c.String(),
                        History = c.String(),
                        Observation = c.String(),
                        Treatment = c.String(),
                        DoctorFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DrugFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RestDuration = c.String(),
                        PrimaryDiagnosis_Id = c.Long(),
                        SecondaryDiagnosis_Id = c.Long(),
                        Status_Id = c.Int(),
                        Patient_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SimpleEntity", t => t.PrimaryDiagnosis_Id)
                .ForeignKey("dbo.SimpleEntity", t => t.SecondaryDiagnosis_Id)
                .ForeignKey("dbo.PatientStatus", t => t.Status_Id)
                .ForeignKey("dbo.Patient", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.PrimaryDiagnosis_Id)
                .Index(t => t.SecondaryDiagnosis_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Prescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Remark = c.String(),
                        PrescriptionType = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Drug_DrugId = c.Long(),
                        PatientVisit_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drug", t => t.Drug_DrugId)
                .ForeignKey("dbo.PatientVisit", t => t.PatientVisit_Id, cascadeDelete: true)
                .Index(t => t.Drug_DrugId)
                .Index(t => t.PatientVisit_Id);
            
            CreateTable(
                "dbo.PatientStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientAdmitance", "Status_Id", "dbo.PatientStatus");
            DropForeignKey("dbo.PatientAdmitance", "PrimaryDiagnosis_Id", "dbo.SimpleEntity");
            DropForeignKey("dbo.PatientVisit", "Patient_Id", "dbo.Patient");
            DropForeignKey("dbo.PatientVisit", "Status_Id", "dbo.PatientStatus");
            DropForeignKey("dbo.PatientVisit", "SecondaryDiagnosis_Id", "dbo.SimpleEntity");
            DropForeignKey("dbo.PatientVisit", "PrimaryDiagnosis_Id", "dbo.SimpleEntity");
            DropForeignKey("dbo.Prescription", "PatientVisit_Id", "dbo.PatientVisit");
            DropForeignKey("dbo.Prescription", "Drug_DrugId", "dbo.Drug");
            DropForeignKey("dbo.Patient", "Location_Id", "dbo.SimpleEntity");
            DropForeignKey("dbo.PatientAdmitance", "Patient_Id", "dbo.Patient");
            DropForeignKey("dbo.MedicalPractitioner", "Location_Id", "dbo.SimpleEntity");
            DropIndex("dbo.Prescription", new[] { "PatientVisit_Id" });
            DropIndex("dbo.Prescription", new[] { "Drug_DrugId" });
            DropIndex("dbo.PatientVisit", new[] { "Patient_Id" });
            DropIndex("dbo.PatientVisit", new[] { "Status_Id" });
            DropIndex("dbo.PatientVisit", new[] { "SecondaryDiagnosis_Id" });
            DropIndex("dbo.PatientVisit", new[] { "PrimaryDiagnosis_Id" });
            DropIndex("dbo.Patient", new[] { "Location_Id" });
            DropIndex("dbo.PatientAdmitance", new[] { "Status_Id" });
            DropIndex("dbo.PatientAdmitance", new[] { "PrimaryDiagnosis_Id" });
            DropIndex("dbo.PatientAdmitance", new[] { "Patient_Id" });
            DropIndex("dbo.MedicalPractitioner", new[] { "Location_Id" });
            DropTable("dbo.PatientStatus");
            DropTable("dbo.Prescription");
            DropTable("dbo.PatientVisit");
            DropTable("dbo.Patient");
            DropTable("dbo.PatientAdmitance");
            DropTable("dbo.SimpleEntity");
            DropTable("dbo.MedicalPractitioner");
            DropTable("dbo.Drug");
        }
    }
}
