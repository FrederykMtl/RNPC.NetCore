using System;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Learning.Emotions;
using RNPC.Core.Memory;

// ReSharper disable InconsistentNaming
namespace RNPC.Tests.Functional.Learning
{
    [TestClass]
    public class MainEmotionLearningStrategyTest
    {
        [TestMethod]
        public void AnalyzeAndLearn_ShortTermAngerRaised_LongTermAngerRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialAnger = character.MyTraits.LongTermEmotions.Anger;

            character.MyTraits.ShortTermEmotions.Anger = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Anger > initialAnger);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Anger);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermAngerLowered_LongTermAngerLowered()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialAnger = character.MyTraits.LongTermEmotions.Anger;

            character.MyTraits.ShortTermEmotions.Anger = 0;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Anger < initialAnger);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermCuriosityRaised_LongTermCuriosityRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialCuriosity = character.MyTraits.LongTermEmotions.Curiosity;

            character.MyTraits.ShortTermEmotions.Curiosity = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Curiosity > initialCuriosity);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Curiosity);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermDisappointmentRaised_LongTermDisappointmentRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialDisappointment = character.MyTraits.LongTermEmotions.Disappointment;

            character.MyTraits.ShortTermEmotions.Disappointment = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Disappointment > initialDisappointment);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Disappointment);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermHappinessRaised_LongTermHappinessRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialHappiness = character.MyTraits.LongTermEmotions.Happiness;

            character.MyTraits.ShortTermEmotions.Happiness = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Happiness > initialHappiness);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Happiness);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermDisgustRaised_LongTermDisgustRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialDisgust = character.MyTraits.LongTermEmotions.Disgust;

            character.MyTraits.ShortTermEmotions.Disgust = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Disgust > initialDisgust);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Disgust);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermJealousyRaised_LongTermJealousyRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialJealousy = character.MyTraits.LongTermEmotions.Jealousy;

            character.MyTraits.ShortTermEmotions.Jealousy = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Jealousy > initialJealousy);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Jealousy);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermPrideRaised_LongTermPrideRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialPride = character.MyTraits.LongTermEmotions.Pride;

            character.MyTraits.ShortTermEmotions.Pride = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Pride > initialPride);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Pride);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermSadnessRaised_LongTermSadnessRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialSadness = character.MyTraits.LongTermEmotions.Sadness;

            character.MyTraits.ShortTermEmotions.Sadness = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Sadness > initialSadness);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Sadness);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermShameRaised_LongTermShameRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialShame = character.MyTraits.LongTermEmotions.Shame;

            character.MyTraits.ShortTermEmotions.Shame = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Shame > initialShame);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Shame);
        }

        [TestMethod]
        public void AnalyzeAndLearn_ShortTermFearRaised_LongTermFearRaised()
        {
            //Arrange
            MainEmotionLearningStrategy strategy = new MainEmotionLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
            int initialFear = character.MyTraits.LongTermEmotions.Fear;

            character.MyTraits.ShortTermEmotions.Fear = 20;
            //ACT
            strategy.AnalyzeAndLearn(character);
            //Assert
            Assert.IsTrue(character.MyTraits.LongTermEmotions.Fear > initialFear);
            Console.WriteLine(@"New value:" + character.MyTraits.LongTermEmotions.Fear);
        }
    }
}
