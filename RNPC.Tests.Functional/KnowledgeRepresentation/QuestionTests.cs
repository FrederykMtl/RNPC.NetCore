using System;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;

namespace RNPC.Tests.Functional.KnowledgeRepresentation
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void InteractWithMe_QuestionWhatIsLove_RightTreeLoadedAndQuetion()
        {
            //ARRANGE
            // ReSharper disable once InconsistentNaming
            var JessicaDay = new Person("Jess", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(JessicaDay, Archetype.TheCaregiver)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            Action question = GetQuestion("Jessica Day", "What is love?");

            //ACT
            var reaction = character.InteractWithMe(question);

            //ASSERT
            Assert.IsNotNull(reaction);
            Assert.AreEqual(question, reaction[0].InitialEvent);
        }

        private Action GetQuestion(string myName, string myQuestion)
        {
            return new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = myQuestion,
                Target = myName,
                EventName = "Question",
                Source = "The Inquisitor"
            };
        }
    }
}
