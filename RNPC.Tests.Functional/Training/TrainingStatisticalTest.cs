using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API.Training;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.Core.Resources;

namespace RNPC.Tests.Functional.Training
{
    [TestClass]
    public class TrainingStatisticalTest : AbstractFunctionalTest
    {
        private class StatisticalResults
        {
            public string Archetype;
            public int YearsofTraining;
            public int QualityIncrease;
            public int QualityDecrease;
            public int QualitiesModified;
            public int ValuesAdded;
            public int DecisionTreesModified; 
            public long TrainingTime;
        }

        [Ignore]
        [TestMethod]
        public void TrainingStatsPerArchetypeFor_1_5_10_Years()
        {
            List<StatisticalResults> completeResults = new List<StatisticalResults>();

            var filePath = ConfigurationDirectory.Instance.LogFilesDirectory + "TrainingStatisticalTestResults.txt";

            if (File.Exists(filePath))
                File.Delete(filePath);

            foreach (var archetype in Enum.GetValues(typeof(Archetype)))
            {
                var typedArchetype = (Archetype) archetype;
                if (typedArchetype == Archetype.None)
                    continue;

                var character = GetCharacterByArchetype(typedArchetype);

                VerifyTestFilesAndDirectory(character.MyName, true);
                completeResults.Add(TrainCharacterFor1Year(character, typedArchetype));
                SetDecisionTreeFilesForCharacter(character, true);
                completeResults.Add(TrainCharacterFor5Years(character, typedArchetype));
                Console.WriteLine(@"10 years");
                SetDecisionTreeFilesForCharacter(character, true);
                completeResults.Add(TrainCharacterFor10Years(character, typedArchetype));
            }

            string[] labels = new string[completeResults.Count];
            long[,] resultTable = new long[completeResults.Count, 7];
            int position = 0;
            foreach (var result in completeResults)
            {
                labels[position] = result.Archetype;
                resultTable[position, 0] = result.YearsofTraining;
                resultTable[position, 1] = result.QualitiesModified;
                resultTable[position, 2] = result.QualityIncrease;
                resultTable[position, 3] = result.QualityDecrease;
                resultTable[position, 4] = result.ValuesAdded;
                resultTable[position, 5] = result.DecisionTreesModified;
                resultTable[position, 6] = result.TrainingTime;
                position++;
            }

            StringBuilder fileContent = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < completeResults.Count; j++)
                {
                    if (i == 0)
                        fileContent.Append(labels[j] + "\t");
                    else
                        fileContent.Append(resultTable[j, i - 1] + "\t");
                }
                fileContent.AppendLine();
            }

            File.AppendAllText(filePath, fileContent.ToString());
        }

        [Ignore]
        [TestMethod]
        public void TrainingStatsPer()
        {
            List<StatisticalResults> completeResults = new List<StatisticalResults>();

            for (int i = 0; i < 36; i++)
            {
                completeResults.Add(new StatisticalResults());
            }
            
            string[] labels = new string[completeResults.Count];
            long[,] resultTable = new long[completeResults.Count, 7];
            int position = 0;
            foreach (var result in completeResults)
            {
                labels[position] = result.Archetype;
                resultTable[position, 0] = result.YearsofTraining;
                resultTable[position, 1] = result.QualitiesModified;
                resultTable[position, 2] = result.QualityIncrease;
                resultTable[position, 3] = result.QualityDecrease;
                resultTable[position, 4] = result.ValuesAdded;
                resultTable[position, 5] = result.DecisionTreesModified;
                resultTable[position, 6] = result.TrainingTime;
                position++;
            }

            StringBuilder fileContent = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < completeResults.Count; j++)
                {
                    if (i == 0)
                        fileContent.Append(labels[0] + "\t");
                    else
                        fileContent.Append(resultTable[j, i - 1] + "\t");
                }
                fileContent.AppendLine();
            }
        }

        /// <summary>
        /// Trains a character for one year
        /// </summary>
        /// <param name="charactertoTrain"></param>
        /// <param name="characterArchetype"></param>
        /// <returns></returns>
        private static StatisticalResults TrainCharacterFor1Year(global::RNPC.Core.Character charactertoTrain, Archetype characterArchetype)
        {
            var preTrainingQs = charactertoTrain.MyTraits.GetPersonalQualitiesValues();
            int numberOfPretrainingValues = charactertoTrain.MyTraits.PersonalValues.Count;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());
            trainer.TrainForAYear(charactertoTrain);

            timer.Stop();

            int qualitiesModified = 0;
            int increaseDelta = 0;
            int decreaseDelta = 0;

            foreach (var quality in charactertoTrain.MyTraits.GetPersonalQualitiesValues())
            {
                if (quality.Value == preTrainingQs[quality.Key])
                    continue;

                qualitiesModified++;

                if (quality.Value > preTrainingQs[quality.Key])
                    increaseDelta += quality.Value - preTrainingQs[quality.Key];
                else
                {
                    decreaseDelta = preTrainingQs[quality.Key] - quality.Value;
                }
            }

            StatisticalResults results = new StatisticalResults
            {
                Archetype = characterArchetype.ToString(),
                YearsofTraining = 1,
                TrainingTime = timer.ElapsedMilliseconds / 1000,
                ValuesAdded = charactertoTrain.MyTraits.PersonalValues.Count - numberOfPretrainingValues,
                QualitiesModified = qualitiesModified,
                QualityIncrease = increaseDelta,
                QualityDecrease = decreaseDelta,
                DecisionTreesModified = GetNumberOfRecentlyChangedDecisionTrees(charactertoTrain.MyName)
            };

            return results;
        }

        /// <summary>
        /// Trains a character for one year
        /// </summary>
        /// <param name="charactertoTrain"></param>
        /// <param name="characterArchetype"></param>
        /// <returns></returns>
        private static StatisticalResults TrainCharacterFor5Years(global::RNPC.Core.Character charactertoTrain, Archetype characterArchetype)
        {
            //ARRANGE
            var preTrainingQs = charactertoTrain.MyTraits.GetPersonalQualitiesValues();
            int numberOfPretrainingValues = charactertoTrain.MyTraits.PersonalValues.Count;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());

            //ACT
            trainer.TrainForXNumberOfYears(charactertoTrain, 5);

            //ASSERT
            timer.Stop();

            int qualitiesModified = 0;
            int increaseDelta = 0;
            int decreaseDelta = 0;

            foreach (var quality in charactertoTrain.MyTraits.GetPersonalQualitiesValues())
            {
                if (quality.Value == preTrainingQs[quality.Key])
                    continue;

                qualitiesModified++;

                if (quality.Value > preTrainingQs[quality.Key])
                    increaseDelta += quality.Value - preTrainingQs[quality.Key];
                else
                {
                    decreaseDelta = preTrainingQs[quality.Key] - quality.Value;
                }
            }

            StatisticalResults results = new StatisticalResults
            {
                Archetype = characterArchetype.ToString(),
                YearsofTraining = 5,
                TrainingTime = timer.ElapsedMilliseconds / 1000,
                ValuesAdded = charactertoTrain.MyTraits.PersonalValues.Count - numberOfPretrainingValues,
                QualitiesModified = qualitiesModified,
                QualityIncrease = increaseDelta,
                QualityDecrease = decreaseDelta,
                DecisionTreesModified = GetNumberOfRecentlyChangedDecisionTrees(charactertoTrain.MyName)
            };

            return results;
        }

        /// <summary>
        /// Trains a character for one year
        /// </summary>
        /// <param name="charactertoTrain"></param>
        /// <param name="characterArchetype"></param>
        /// <returns></returns>
        private static StatisticalResults TrainCharacterFor10Years(global::RNPC.Core.Character charactertoTrain, Archetype characterArchetype)
        {
            //ARRANGE
            var preTrainingQs = charactertoTrain.MyTraits.GetPersonalQualitiesValues();
            int numberOfPretrainingValues = charactertoTrain.MyTraits.PersonalValues.Count;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());

            //ACT
            trainer.TrainForXNumberOfYears(charactertoTrain, 10);

            //ASSERT
            timer.Stop();

            int qualitiesModified = 0;
            int increaseDelta = 0;
            int decreaseDelta = 0;

            foreach (var quality in charactertoTrain.MyTraits.GetPersonalQualitiesValues())
            {
                if (quality.Value == preTrainingQs[quality.Key])
                    continue;

                qualitiesModified++;

                if (quality.Value > preTrainingQs[quality.Key])
                    increaseDelta += quality.Value - preTrainingQs[quality.Key];
                else
                {
                    decreaseDelta = preTrainingQs[quality.Key] - quality.Value;
                }
            }

            StatisticalResults results = new StatisticalResults
            {
                Archetype = characterArchetype.ToString(),
                YearsofTraining = 10,
                TrainingTime = timer.ElapsedMilliseconds / 1000,
                ValuesAdded = charactertoTrain.MyTraits.PersonalValues.Count - numberOfPretrainingValues,
                QualitiesModified = qualitiesModified,
                QualityIncrease = increaseDelta,
                QualityDecrease = decreaseDelta,
                DecisionTreesModified = GetNumberOfRecentlyChangedDecisionTrees(charactertoTrain.MyName)
            };

            return results;
        }
    }
}
