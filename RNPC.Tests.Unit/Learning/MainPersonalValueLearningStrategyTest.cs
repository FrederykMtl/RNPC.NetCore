using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.Learning;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.Values;
using RNPC.Core.Memory;
using RNPC.FileManager;

// ReSharper disable InconsistentNaming

namespace RNPC.Tests.Unit.Learning
{
    [TestClass]
    public class MainPersonalValueLearningStrategyTest
    {
        [TestMethod]
        public void AnalyzeAndLearn_ThreeDeterminationTests_NewValueAdded30PercentOfTheTime()
        {
            //ARRANGE
            var learningStrategy = new MainPersonalValueLearningStrategy(new PersonalValueAssociations());

            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            int[] compiledResults = {0,0};

            //ACT
            for (int i = 0; i < 1000; i++)
            {
                var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder()
                };

                List<NodeTestInfo> nodeTests = new List<NodeTestInfo>
                {
                    new NodeTestInfo
                    {
                        CharacteristicName = "Determination",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Determination",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Determination",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    }
                };

                character.MyMemory.AddNodeTestResults(nodeTests);

                bool result = RunAddingValueLearningTest(character, learningStrategy);

                if (result)
                    compiledResults[0]++;
                else
                {
                    compiledResults[1]++;
                }
            }

            //ASSERT
            //Stat is 30%. with random we allow +-2%
            Assert.IsTrue(compiledResults[0] < 320);
            Assert.IsTrue(compiledResults[0] > 280);
        }

        [TestMethod]
        public void AnalyzeAndLearn_NRVAValuesTests_NewValueAdded2PercentOfTheTime()
        {
            //ARRANGE
            var learningStrategy = new MainPersonalValueLearningStrategy(new PersonalValueAssociations());

            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            int[] compiledResults = { 0, 0 };

            //ACT
            for (int i = 0; i < 1000; i++)
            {
                var character = new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder()
                };

                List<NodeTestInfo> nodeTests = new List<NodeTestInfo>
                {
                    new NodeTestInfo
                    {
                        CharacteristicName = "Determination",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = false,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Determination",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = false,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Determination",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = false,
                        AttributeValue = 0,
                        Modifier = 0
                    }
                };

                character.MyMemory.AddNodeTestResults(nodeTests);

                bool result = RunAddingValueLearningTest(character, learningStrategy);

                if (result)
                    compiledResults[0]++;
                else
                {
                    compiledResults[1]++;
                }
            }

            //ASSERT
            //Stat is 2%. with random we allow +-2%
            Assert.IsTrue(compiledResults[0] < 40);
            Assert.IsTrue(compiledResults[0] > 0);
        }

        private static bool RunAddingValueLearningTest(global::RNPC.Core.Character character, ILearningStrategy learningStrategy)
        {
            int nbOfValues = character.MyTraits.PersonalValues.Count;
            //Act
            learningStrategy.AnalyzeAndLearn(character);
            //Assert
            return nbOfValues < character.MyTraits.PersonalValues.Count;
        }

        [TestMethod]
        public void AnalyzeAndLearn_EightAchievementTests_NewValueRemoved30PercentOfTheTime()
        {
            //ARRANGE
            var learningStrategy = new MainPersonalValueLearningStrategy(new PersonalValueAssociations());

            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            int[] compiledResults = { 0, 0 };

            //ACT
            for (int i = 0; i < 1000; i++)
            {
                var character = new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder()
                };
                //PersonalValues.Achievement
                List<NodeTestInfo> nodeTests = new List<NodeTestInfo>
                {
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    },
                    new NodeTestInfo
                    {
                        CharacteristicName = "Achievement",
                        TestedCharacteristic = CharacteristicType.Values,
                        Result = true,
                        AttributeValue = 0,
                        Modifier = 0
                    }
                };

                character.MyMemory.AddNodeTestResults(nodeTests);

                bool result = RunRemovingValueLearningTest(character, learningStrategy);

                if (result)
                    compiledResults[0]++;
                else
                {
                    compiledResults[1]++;
                }
                //We add it back to the list
                character.MyTraits.PersonalValues.Add(PersonalValues.Achievement);
            }

            //ASSERT
            //Stat is 30%. with random we allow +-3.5%
            //if(compiledResults[0] > 330)
            //    Console.WriteLine(compiledResults[0]);
            Assert.IsTrue(compiledResults[0] <= 335);
            //if (compiledResults[0] < 270)
            //    Console.WriteLine(compiledResults[0]);
            Assert.IsTrue(compiledResults[0] >= 265);
        }

        private static bool RunRemovingValueLearningTest(global::RNPC.Core.Character character, ILearningStrategy learningStrategy)
        {
            int nbOfValues = character.MyTraits.PersonalValues.Count;
            //Act
            learningStrategy.AnalyzeAndLearn(character);
            //Assert
            return nbOfValues > character.MyTraits.PersonalValues.Count;
        }
    }
}
