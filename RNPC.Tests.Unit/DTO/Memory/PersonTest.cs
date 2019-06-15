using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.GameTime;
using RNPC.Core.Memory;

namespace RNPC.Tests.Unit.DTO.Memory
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void Age_PersonWithDOB_ValidAgeReturned()
        {
            //ARRANGE
            Person person = new Person("That Dude", Guid.NewGuid());
            CustomDateTime dateOfBirth = new CustomDateTime(3876, 10, 10);
            person.DateOfBirth = dateOfBirth;

            CustomDateTime currentGameTime = new CustomDateTime(3917, 7, 30);
            //ACT
            int age = person.Age(currentGameTime);
            //ASSERT
            Assert.AreEqual(40, age);
        }

        [TestMethod]
        public void Age_DeadPerson_ValidAgeReturned()
        {
            //ARRANGE
            Person person = new Person("That Other Dude", Guid.NewGuid());
            //His date of birth was unrecorded
            CustomDateTime dateOfBirth = new CustomDateTime(3876);
            CustomDateTime dateOfDeath = new CustomDateTime(3946, 5, 3);

            person.DateOfBirth = dateOfBirth;
            person.DateOfDeath = dateOfDeath;

            CustomDateTime currentGameTime = new CustomDateTime(4388, 1, 11);
            //ACT
            int age = person.Age(currentGameTime);
            //ASSERT
            Assert.AreEqual(70, age);
        }
    }
}
