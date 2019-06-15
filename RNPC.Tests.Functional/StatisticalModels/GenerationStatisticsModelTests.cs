using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using RNPC.Core.Resources;

namespace RNPC.Tests.Functional.StatisticalModels
{
    [Ignore]
    [TestClass]
    public class GenerationStatisticsModelTests
    {
        [TestMethod]
        public void RandomGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.None, SAMPLING);
        }
        [TestMethod]
        public void TheCaregiverGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheCaregiver, SAMPLING);
        }
        [TestMethod]
        public void TheCreatorGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheCreator, SAMPLING);
        }
        [TestMethod]
        public void TheDestroyerGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheDestroyer, SAMPLING);
        }
        [TestMethod]
        public void TheInnocentGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheInnocent, SAMPLING);
        }
        [TestMethod]
        public void TheJesterGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheJester, SAMPLING);
        }

        [TestMethod]
        public void TheLoverGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheLover, SAMPLING);
        }

        [TestMethod]
        public void TheMagicianGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheMagician, SAMPLING);
        }

        [TestMethod]
        public void TheOrphanGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheOrphan, SAMPLING);
        }

        [TestMethod]
        public void TheRulerGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheRuler, SAMPLING);
        }

        [TestMethod]
        public void TheSageGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheSage, SAMPLING);
        }

        [TestMethod]
        public void TheSeekerGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheSeeker, SAMPLING);
        }

        [TestMethod]
        public void TheWarriorGeneration_SamplingNumber_CharacterQualityGenerationStatistics()
        {
            int SAMPLING = 100000;
            CharacterGenerationByArchetype(Archetype.TheWarrior, SAMPLING);
        }

        /// <summary>
        /// Generates a number of characters of an archetype type for statistical models
        /// </summary>
        /// <param name="archetype">Archetype name</param>
        /// <param name="sampling"></param>
        private void CharacterGenerationByArchetype(Archetype archetype, int sampling)
        {
        //ARRANGE
            var strongPointStats = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var weakPointStats = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int numberOfOutliers = 0;

            //ACT
            for (int i = 0; i < sampling; i++)
            {
                Person newPerson = new Person("Character" + i, Guid.NewGuid());
                var character = new global::RNPC.Core.Character(newPerson, archetype)
                {
                    FileController = new DecisionTreeFileController(),
                    DecisionTreeBuilder = new DecisionTreeBuilder()
                };

                int numberOfStrongPoints = CalculateNbOfStrongPoints(character.MyTraits);
                int numberOfWeakPoints = CalculateNbOfWeakPoints(character.MyTraits);

                if (numberOfStrongPoints > 14)
                    numberOfOutliers++;
                else
                    strongPointStats[numberOfStrongPoints]++;

                if (numberOfWeakPoints > 14)
                    numberOfOutliers++;
                else
                    weakPointStats[numberOfWeakPoints]++;
            }

            //DISPLAY
            for (int i = 0; i < strongPointStats.Count; i++)
            {
                //double strongPercentage = strongPointStats[i] / (double)SAMPLING * 100;
                //Debug.WriteLine("Traits generated with {0} strong points: {1} -- percentage {2}%", i, strongPointStats[i], strongPercentage);
                //double weakPercentage = weakPointStats[i] / (double)SAMPLING * 100;
                //Debug.WriteLine("Traits generated with {0} weak points: {1} -- percentage {2}%", i, weakPointStats[i], weakPercentage);

                Debug.WriteLine("{0}, {1}, {2}", i, strongPointStats[i], weakPointStats[i]);
            }

            double outlierPercentage = numberOfOutliers / (double)sampling * 100;
            Debug.WriteLine("I have generated {0}% outliers", outlierPercentage);

            Assert.IsTrue(outlierPercentage < 0.5);
        }

        private int CalculateNbOfWeakPoints(CharacterTraits traits)
        {
            return traits.GetPersonalQualitiesValues().Count(q => q.Value <= Constants.MaxWeakPoint);
        }

        private int CalculateNbOfStrongPoints(CharacterTraits traits)
        {
            return traits.GetPersonalQualitiesValues().Count(q => q.Value >= Constants.MinStrongPoint);
        }
        [Ignore]
        [TestMethod]
        public void RandomGeneration_SamplingNumber_CharacterIdentityGenerationStatistics()
        {
            //ARRANGE
            int SAMPLING = 100000;
            List<int> genderStats = new List<int> { 0, 0, 0, 0, 0, 0 };
            List<int> sexStats = new List<int> { 0, 0, 0, 0, 0, 0 };
            List<int> orientationStats = new List<int> { 0, 0, 0, 0, 0, 0 };

            //ACT
            for (int i = 0; i < SAMPLING; i++)
            {
                Person newPerson = new Person("Character" + i, Guid.NewGuid());

                var character = new global::RNPC.Core.Character(newPerson, Archetype.None)
                    {
                        FileController = new DecisionTreeFileController(),
                        DecisionTreeBuilder = new DecisionTreeBuilder()
                    };

                sexStats[(int)character.MyTraits.Sex]++;
                genderStats[(int)character.MyTraits.Gender]++;
                orientationStats[(int)character.MyTraits.Orientation]++;
            }

            Debug.WriteLine("No Gender Sex Orientation");
            //DISPLAY
            for (int i = 0; i < 6; i++)
            {
                Debug.WriteLine("{0}, {1}, {2}, {3}", i, genderStats[i], sexStats[i], orientationStats[i]);
            }

            int femaleGenderDemo = int.Parse(Demographics.Gender_female);
            int maleSexDemo = int.Parse(Demographics.Sex_male);
            int intersexDemo = int.Parse(Demographics.Sex_intersex);
            int bisexualDemo = int.Parse(Demographics.Orientation_bi);
            int pansexualDemo = int.Parse(Demographics.Orientation_pansexual);

            double femaleDeviation = Math.Abs(femaleGenderDemo - (double)genderStats[1] / 1000);
            Assert.IsTrue(femaleDeviation <= 0.25);

            double maleDeviation = Math.Abs(maleSexDemo - (double)sexStats[0] / 1000);
            Assert.IsTrue(maleDeviation <= 0.25);

            double intersexDeviation = Math.Abs(intersexDemo - (double)sexStats[2] / 1000);
            Assert.IsTrue(intersexDeviation <= 0.2);

            double bisexualDeviation = Math.Abs(bisexualDemo - (double)orientationStats[2] / 1000);
            Assert.IsTrue(bisexualDeviation <= 0.2);

            double pansexualDeviation = Math.Abs(pansexualDemo - (double)orientationStats[4] / 1000);
            Assert.IsTrue(pansexualDeviation <= 0.2);
        }
    }
}
