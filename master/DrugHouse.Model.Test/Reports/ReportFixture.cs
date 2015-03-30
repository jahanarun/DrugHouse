using DrugHouse.Model.Enum;
using DrugHouse.Model.Reports;
using DrugHouse.Model.Types;
using NUnit.Framework;

namespace DrugHouse.Model.Test.Reports
{
    [TestFixture]
    public class ReportFixture
    {

        private Report TestClass;
        [SetUp]
        public void Setup()
        {
            TestClass = new Report();
        }

        [Test]
        public void SampleTest()
        {
            TestClass.CreateDocument(DocumentType.MedicalCertificate, new PatientVisit(new Patient()));
        }
    }
}
