/*  DrugHouse - An Hospital management software
    Copyright (C) {2015}  {Jahan Arun, J}     */

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Repositories
{
	public class DrugHouseContext : DbContext
	{
		public DrugHouseContext(string conn)
			: base(conn)
		{
			Database.SetInitializer(new DrugHouseContextInitializer());
			this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<Patient> Patients { get; set; }
		public DbSet<MedicalPractitioner> MedicalPractitioners { get; set; }
		public DbSet<Drug> Drugs { get; set; }
		public DbSet<PatientVisit> PatientVisits { get; set; }
		public DbSet<PatientAdmitance> PatientAdmitances { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<SimpleEntity> SimpleEntities { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Entity<Patient>().HasMany(e=>e.Visits)
							.WithOptional(s=>s.Patient).WillCascadeOnDelete(true);
			modelBuilder.Entity<PatientVisit>().HasMany(e=>e.Prescriptions)
							.WithOptional(s=>s.PatientVisit).WillCascadeOnDelete(true);

			//modelBuilder.Entity<Patient>()
			//    .Map(map =>
			//    {
			//        map.Properties(p => new
			//        {
			//            p.Id,
			//            p.Name,
			//            p.Gender,
			//            p.Age,
			//            p.GuardianName,
			//            p.GuardianRelationship,
			//            p.Remark,
			//        });
			//        map.ToTable("Patient");
			//    })
			//    .Map(map =>
			//    {
			//        map.Properties(p => new
			//        {
			//            p.Id,
			//            p.Address,
			//            p.Location,
			//            p.Email,
			//            p.PhoneNumber
			//        });
			//        map.ToTable("PatientContactDetail");
			//    });

			//modelBuilder.Entity<Patient>().HasMany(v => v.Visits);

		}

	}

	/// <summary>
	///  This hack is to keep System.Data.Entity.SqlServer Namespace included in this file so that DbContext works properly.
	/// </summary>
	internal static class MissingDllHack
	{
		private static SqlProviderServices instance = SqlProviderServices.Instance;
	}
}