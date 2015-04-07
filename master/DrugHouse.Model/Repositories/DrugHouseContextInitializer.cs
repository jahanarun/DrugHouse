/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Data.Entity;
using DrugHouse.Model.Types;
using DrugHouse.Shared.Enumerations;

namespace DrugHouse.Model.Repositories
{
    public class DrugHouseContextInitializer : CreateDatabaseIfNotExists<DrugHouseContext>
//: DropCreateDatabaseIfModelChanges<DrugHouseContext>
    {
        protected override void Seed(DrugHouseContext context)
        {
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Patient, RESEED, 1000)");

            context.Patients.Add(new Patient() { Name = "Test1" });
            context.Patients.Add(new Patient() {Name = "Test2"});
            context.Patients.Add(new Patient() {Name = "Test3"});

            context.Drugs.Add(new Drug() {Name = "Drug1", DrugType = DrugType.Capsule});
            context.Drugs.Add(new Drug() {Name = "Drug2", DrugType = DrugType.Tablet});
            context.Drugs.Add(new Drug() {Name = "Drug3", DrugType = DrugType.Syrup});
            context.Drugs.Add(new Drug() {Name = "Drug4", DrugType = DrugType.Capsule});
            context.Drugs.Add(new Drug() {Name = "Syringe1", DrugType = DrugType.Syringe});
            context.Drugs.Add(new Drug() {Name = "Syringe2", DrugType = DrugType.Syringe});

            context.SimpleEntities.Add(new SimpleEntity(SimpleEntity.Markers.Diagnosis) { Name = "Diagnosis1" });
            context.SimpleEntities.Add(new SimpleEntity(SimpleEntity.Markers.Diagnosis) { Name = "Diagnosis2" });
            context.SimpleEntities.Add(new SimpleEntity(SimpleEntity.Markers.Diagnosis) { Name = "Diagnosis3" });
            context.SimpleEntities.Add(new SimpleEntity(SimpleEntity.Markers.Diagnosis) { Name = "Diagnosis4" });
            context.SimpleEntities.Add(new SimpleEntity(SimpleEntity.Markers.Diagnosis) { Name = "Diagnosis5" });
            context.SimpleEntities.Add(new SimpleEntity(SimpleEntity.Markers.Diagnosis) { Name = "Diagnosis6" });

            base.Seed(context);
        }
    }
}
