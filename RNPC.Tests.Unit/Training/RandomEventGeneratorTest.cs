using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API.Training;
using RNPC.Core.Action;

namespace RNPC.Tests.Unit.Training
{
    [TestClass]
    public class RandomEventGeneratorTest
    {
        [TestMethod]
        public void GetRandomEvent_NoParam_GetRandomEvent()
        {
            //ARRANGE
            //ACT
            var testEvent = RandomEventGenerator.GetRandomEvent();
            //ASSERT
            Assert.IsNotNull(testEvent);
            Assert.IsFalse(string.IsNullOrEmpty(testEvent.EventName));
        }

        [TestMethod]
        public void GetRandomEvent_100Iterations_Get100DifferentRandomEvent()
        {
            //ARRANGE
            List<Action> actions = new List<Action>();
            //ACT
            for (int i = 0; i < 100; i++)
            {
                actions.Add(RandomEventGenerator.GetRandomEvent());
            }

            //ASSERT
            Assert.IsNotNull(actions);
            Assert.AreEqual(100, actions.Count);

            var events = actions.Select(a => a.EventName).Distinct().ToList();
            Assert.IsTrue(events.Count > 7);
            Assert.IsTrue(events.Count <= 10);
        }


    }
}
