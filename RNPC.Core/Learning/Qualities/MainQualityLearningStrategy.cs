using System;
using System.Linq;
using System.Reflection;
using RNPC.Core.Enums;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.Resources;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;

// ReSharper disable InlineOutVariableDeclaration
namespace RNPC.Core.Learning.Qualities
{
    internal class MainQualityLearningStrategy : ILearningStrategy
    {
        public bool AnalyzeAndLearn(Character learningCharacter)
        {
            var testResults = learningCharacter.MyMemory.GetAllCurrentNodeTestInfos();

            var testedQualities = testResults.Where(t => t.TestedCharacteristic == CharacteristicType.Quality).ToList();

            int qualityEvolveRate = LearningParameters.QualityLearningRate;
            int qualityDevolveRate = LearningParameters.QualityDevolveRate;
          
            //If there are not enough nodes for change we stop here.
            if (testedQualities.Count < LearningParameters.QualityLearningThreshold)
                return true;

            var groupedQualitiesTest = testedQualities.GroupBy(q => q.CharacteristicName).ToList();

            foreach (var testInfos in groupedQualitiesTest)
            {
                if(testInfos.Count() < LearningParameters.QualityLearningThreshold)
                    continue;

                int failedTests = testInfos.Count(q => q.Result == false);
                int succeededTests = testInfos.Count(q => q.Result);

                //if they're identical or almost identical it cancels out.
                if (failedTests == succeededTests || Math.Abs(failedTests - succeededTests) == 1)
                {
                    continue;
                }

                string quality = testInfos.ToList()[0].CharacteristicName;
                Type type = learningCharacter.MyTraits.GetType();
                PropertyInfo info = type.GetProperty(quality);

                if (info == null)
                    return false;

                int currentTraitValue = (int) info.GetValue(learningCharacter.MyTraits);

                if (currentTraitValue <= 1 || currentTraitValue >= 100)
                    continue;

                if (failedTests > succeededTests)
                {
                    //if you lose the lottery you go down!
                    if (RandomValueGenerator.GeneratePercentileIntegerValue() <= qualityDevolveRate)
                        info.SetValue(learningCharacter.MyTraits, currentTraitValue - 1);
                }

                //This makes it linearly harder to evolve a strong point.
                if (currentTraitValue >= Constants.MinStrongPoint && Constants.MaxStrongPoint - currentTraitValue < qualityEvolveRate)
                {
                    qualityEvolveRate = Constants.MaxStrongPoint - currentTraitValue;
                }

                //This makes it harder to evolve out of a weak point.
                if (currentTraitValue <= Constants.MaxWeakPoint && currentTraitValue < qualityEvolveRate)
                {
                    qualityEvolveRate = currentTraitValue;
                }

                //if you win the lottery you go up!
                if (RandomValueGenerator.GeneratePercentileIntegerValue() >= 100 - qualityEvolveRate)
                info.SetValue(learningCharacter.MyTraits, currentTraitValue + 1);
            }

            return true;
        }
    }
}