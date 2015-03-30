using System.Linq;
using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using DrugHouse.Shared.Enumerations;
using NUnit.Framework;

namespace DrugHouse.Model.Test.Repositorites
{
    [TestFixture]
    class RepositoryFixture
    {
        private IDataAccess TestClass;
        private Patient Patient;

        [SetUp]
        public void Setup()
        {
            TestClass = Model.DataAccess.GetInstance();
            
        }

        private static Patient GetTestPatient()
        {
            var result = new Patient() {Name = "Test", Address = "Test Address", Gender = GenderType.Male};
            result.ResetDbStatus();
            return result;
        }

        private Patient CreateTestPatientInDb()
        {
            Patient result = GetTestPatient();
            result.DbStatus = RepositoryStatus.New;
            Patient = TestClass.SavePatient(result);
            return result;
        }

        private void DeleteTestPatientInDb(Patient p)
        {
            p.DbStatus = RepositoryStatus.Deleted;
            Patient = TestClass.SavePatient(p);
        }

        [Test]
        public void AddAndDeletePatientTest()
        {
            //Arrange
            Patient = GetTestPatient();

            //Act
            var beforeAdd = TestClass.GetAllPatients().Count;
            Patient.DbStatus = RepositoryStatus.New;
            Patient = TestClass.SavePatient(Patient);
            var afterAdd = TestClass.GetAllPatients().Count;


            //Assert
            Assert.AreEqual(beforeAdd + 1, afterAdd);

            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void RemovePatientTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();

            //Act
            var beforeDelete = TestClass.GetAllPatients().Count;
            Patient.DbStatus = RepositoryStatus.Deleted;
            TestClass.SavePatient(Patient);
            var afterDelete = TestClass.GetAllPatients().Count;


            //Assert
            Assert.AreEqual(beforeDelete - 1, afterDelete);
        }


        [Test]
        public void EditPatientTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();

            //Act
            Patient.Name = "New Edit Name";
            Patient.DbStatus = RepositoryStatus.Edited;
            TestClass.SavePatient(Patient);
            var newPatient = TestClass.GetPatientDetails(Patient.Id);


            //Assert
            Assert.AreEqual(newPatient.Name, "New Edit Name");

            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void AddVisitTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();

            //Act
            Patient.AddCase(typeof(PatientVisit));
            Patient=TestClass.SavePatient(Patient);

            //Assert
            Assert.AreEqual(1,Patient.Visits.Count);

            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void RemoveVisitTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();
            Patient.AddCase(typeof(PatientVisit));
            Patient = TestClass.SavePatient(Patient);

            //Act
            Patient.RemoveCase(Patient.Visits.First());
            Patient = TestClass.SavePatient(Patient);

            //Assert
            Assert.AreEqual(0, Patient.Visits.Count);

            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void EditVisitTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();
            Patient.AddCase(typeof(PatientVisit));
            Patient = TestClass.SavePatient(Patient);

            //Act
            //Patient.Visits.First().Fee = new PatientFee(){DoctorFee = 100};
            Patient.DbStatus=RepositoryStatus.Edited;
            Patient = TestClass.SavePatient(Patient);

            //Assert
            //Assert.AreEqual(100, Patient.Visits.First().Fee.DoctorFee);

            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void AddPrescriptionTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();
            Patient.AddCase(typeof(PatientVisit));
            Patient = TestClass.SavePatient(Patient);

            //Act
            Patient.Visits.First().AddPrescription();
            Patient = TestClass.SavePatient(Patient);

            //Assert
            Assert.AreEqual(1, Patient.Visits.First().Prescriptions.Count);

            //Cleanup
            DeleteTestPatientInDb(Patient);
        }
        [Test]
        public void AddDrugTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();
            Patient.AddCase(typeof(PatientVisit));
            Patient = TestClass.SavePatient(Patient);

            //Act
            var prescription = Patient.Visits.First().AddPrescription();
            Patient = TestClass.SavePatient(Patient);

            var myDrug = TestClass.GetDrugs().First();
            prescription.Drug = myDrug;
            Patient = TestClass.SavePatient(Patient);

            //Assert
            Assert.AreEqual(myDrug.DrugId, Patient.Visits.First().Prescriptions.First().Drug.DrugId);
            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void RemoveDrugTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();
            Patient.AddCase(typeof(PatientVisit));
            var prescription = Patient.Visits.First().AddPrescription();
            prescription.Drug = TestClass.GetDrugs().First();
            Patient = TestClass.SavePatient(Patient);

            //Act
            Patient.Visits.First().Prescriptions.First().Drug = null;
            Patient.DbStatus=RepositoryStatus.Edited;
            Patient = TestClass.SavePatient(Patient);

            //Assert
            Assert.Null(Patient.Visits.First().Prescriptions.First().Drug);
            //Cleanup
            DeleteTestPatientInDb(Patient);
        }

        [Test]
        public void EditDrugTest()
        {
            //Arrange
            Patient = CreateTestPatientInDb();
            Patient.AddCase(typeof(PatientVisit));
            var prescription = Patient.Visits.First().AddPrescription();
            var myDrug = TestClass.GetDrugs().First();
            prescription.Drug = myDrug;
            Patient = TestClass.SavePatient(Patient);

            //Act
            var newDrug = TestClass.GetDrugs()[2];
            Patient.Visits.First().Prescriptions.First().Drug = newDrug;
            Patient.DbStatus = RepositoryStatus.Edited;
            Patient = TestClass.SavePatient(Patient);

            //Assert
            Assert.AreEqual(newDrug.DrugId, Patient.Visits.First().Prescriptions.First().Drug.DrugId);
            //Cleanup
            DeleteTestPatientInDb(Patient);
        }
    }
}
