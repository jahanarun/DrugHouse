using DrugHouse.Model.Enum;
using DrugHouse.Shared.Enumerations;
using NUnit.Framework;
using DrugHouse.Model.Types;

namespace DrugHouse.Model.Test.Types
{
    [TestFixture]
    class MedicalPractitionerFixture
    {
        private MedicalPractitioner testClass;
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void CategoryTest()
        {
            //Arrange
            testClass = new MedicalPractitioner(2);

            //Assert
            Assert.AreEqual(PersonType.MedicalPractitioner, testClass.Category);
        }

    }
}
