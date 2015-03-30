using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugHouse.Model.Types;
using NUnit.Framework;

namespace DrugHouse.Model.Test
{
    class DataAccessFixture
    {
        private DataAccess TestClass;

        [SetUp]
        public void Setup()
        {
            TestClass = (DataAccess) DataAccess.GetInstance();
        }

        [Test]
        public void GetApplicationConnectionStringEncryptedTest()
        {
            //Arrange

            //Act
            var result = DataAccess.GetApplicationConnectionStringEncrypted();

            //Assert
            Assert.AreEqual(
                "b99inbkrvHUlp6+LmIYP4FqywDggaUuR6gPm+I1u5aIzWbuFffipwabGxkPNx43dZd6RUxEP0QPtFT5m5MtRus12eST/IUCvjTKutxiSBeeIcXJCfhNyYAJL79S9pBXy",
                result);

        }
    }
}
