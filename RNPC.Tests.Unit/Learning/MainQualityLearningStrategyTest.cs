using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Learning.Qualities;
using RNPC.Core.Memory;
using RNPC.FileManager;

// ReSharper disable InconsistentNaming

namespace RNPC.Tests.Unit.Learning
{
    [TestClass]
    public class MainQualityLearningStrategyTest
    {
        [TestMethod]
        public void AnalyzeAndLearn_NodeResultAllPositive_WillpowerWentUp()
        {
            //ARRANGE
            MainQualityLearningStrategy learningStrategy = new MainQualityLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            int[] compiledResults = {0,0};

            //ACT
            for (int i = 0; i < 1000; i++)
            {

                var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder()
                };

                List<NodeTestInfo> nodeTests = new List<NodeTestInfo>
                {
                    new NodeTestInfo
                    {
                        CharacteristicName = "Willpower",
                        TestedCharacteristic = CharacteristicType.Quality,
                        Result = true,
                        AttributeValue = character.MyTraits.Willpower,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Willpower",
                        TestedCharacteristic = CharacteristicType.Quality,
                        Result = true,
                        AttributeValue = character.MyTraits.Willpower,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Willpower",
                        TestedCharacteristic = CharacteristicType.Quality,
                        Result = true,
                        AttributeValue = character.MyTraits.Willpower,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Willpower",
                        TestedCharacteristic = CharacteristicType.Quality,
                        Result = true,
                        AttributeValue = character.MyTraits.Willpower,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Willpower",
                        TestedCharacteristic = CharacteristicType.Quality,
                        Result = true,
                        AttributeValue = character.MyTraits.Willpower,
                        Modifier = 0
                    }
                };

                character.MyMemory.AddNodeTestResults(nodeTests);

                var willpower = character.MyTraits.Willpower;

                bool result = RunIncreaseQualityLearningTest(character, learningStrategy, willpower, "Willpower");

                if (result)
                    compiledResults[0]++;
                else
                {
                    compiledResults[1]++;
                }
            }

            //ASSERT
            //Stat is 20%. with random we allow +-2%
            Assert.IsTrue(compiledResults[0] < 220);
            Assert.IsTrue(compiledResults[0] > 180);
        }

        private static bool RunIncreaseQualityLearningTest(global::RNPC.Core.Character character, MainQualityLearningStrategy learningStrategy, int quality, string qualityName)
        {
            //Act
            learningStrategy.AnalyzeAndLearn(character);

            Type type = character.MyTraits.GetType();
            PropertyInfo info = type.GetProperty(qualityName);

            if (info == null)
                return false;

            int newValue = (int)info.GetValue(character.MyTraits);
            //Assert
            return quality < newValue;
        }

        [TestMethod]
        public void AnalyzeAndLearn_NodeResultAllNegative_WillpowerWentDown()
        {
            //ARRANGE
            MainQualityLearningStrategy learningStrategy = new MainQualityLearningStrategy();

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };

            List<NodeTestInfo> nodeTests = new List<NodeTestInfo>
            {
                new NodeTestInfo
                {
                    CharacteristicName = "Willpower",
                    TestedCharacteristic = CharacteristicType.Quality,
                    Result = false,
                    AttributeValue = character.MyTraits.Willpower,
                    Modifier = 0
                },
                new NodeTestInfo
                {
                    CharacteristicName = "Willpower",
                    TestedCharacteristic = CharacteristicType.Quality,
                    Result = false,
                    AttributeValue = character.MyTraits.Willpower,
                    Modifier = 0
                },
                new NodeTestInfo
                {
                    CharacteristicName = "Willpower",
                    TestedCharacteristic = CharacteristicType.Quality,
                    Result = false,
                    AttributeValue = character.MyTraits.Willpower,
                    Modifier = 0
                },
                new NodeTestInfo
                {
                    CharacteristicName = "Willpower",
                    TestedCharacteristic = CharacteristicType.Quality,
                    Result = false,
                    AttributeValue = character.MyTraits.Willpower,
                    Modifier = 0
                },
                new NodeTestInfo
                {
                    CharacteristicName = "Willpower",
                    TestedCharacteristic = CharacteristicType.Quality,
                    Result = false,
                    AttributeValue = character.MyTraits.Willpower,
                    Modifier = 0
                }
            };

            character.MyMemory.AddNodeTestResults(nodeTests);

            int[] compiledResults = { 0, 0 };

            //ACT
            for (int i = 0; i < 1000; i++)
            {

                var willpower = character.MyTraits.Willpower;

                bool result = RunDecreaseQualityLearningTest(character, learningStrategy, willpower, "Willpower");

                if (result)
                    compiledResults[0]++;
                else
                {
                    compiledResults[1]++;
                }
            }

            //ASSERT
            //Stat is 22%. with random we allow +-2%
            Assert.IsTrue(compiledResults[0] < 240);
            Assert.IsTrue(compiledResults[0] > 200);
        }


        private static bool RunDecreaseQualityLearningTest(global::RNPC.Core.Character character, MainQualityLearningStrategy learningStrategy, int quality, string qualityName)
        {
            //Act
            learningStrategy.AnalyzeAndLearn(character);

            Type type = character.MyTraits.GetType();
            PropertyInfo info = type.GetProperty(qualityName);

            if (info == null)
                return false;

            int newValue = (int)info.GetValue(character.MyTraits);
            //Assert
            return quality > newValue;
        }
    }
}
