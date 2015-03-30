using DrugHouse.Model.Enum;
using DrugHouse.Model.Types;
using NUnit.Framework;
namespace DrugHouse.Model.Test.Types
{
    [TestFixture]
    public class PatientFixture
    {
        private Patient testClass;
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void CategoryTest()
        {
            //Act
            testClass = new Patient(3);
            //Assert
            Assert.AreEqual(PersonType.Patient, testClass.Category);
        }
    }
}
