using System;
using System.Linq;
using RNPC.Core.Enums;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Learning.Values
{
    internal class MainPersonalValueLearningStrategy : ILearningStrategy
    {
        private readonly IPersonalValueAssociations _personalValueAssociations;

        public MainPersonalValueLearningStrategy(IPersonalValueAssociations personalValueAssociations)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (personalValueAssociations == null)
                throw new ArgumentNullException(nameof(personalValueAssociations), @"Personal values associations can not be null");

            _personalValueAssociations = personalValueAssociations;
        }

        public bool AnalyzeAndLearn(Character learningCharacter)
        {
            var nodeTestResults = learningCharacter.MyMemory.GetAllCurrentNodeTestInfos();

            var testedValues = nodeTestResults.Where(t => t.TestedCharacteristic == CharacteristicType.Values).ToList();

            //If there are not enough nodes for change we stop here.
            if (testedValues.Count < LearningParameters.MinimumValueThreshold)
                return false;

            var groupedValueTests = testedValues.GroupBy(q => q.CharacteristicName).ToList();

            foreach (var testInfos in groupedValueTests)
            {
                var testInfo = testInfos.ToList()[0];

                if (testInfo.Result)
                {
                    if (testInfos.Count() < LearningParameters.MinimumValueThreshold)
                        continue;

                    if (testInfos.Count() >= LearningParameters.ValueLossThreshold)
                    {
                        //if you win the lottery you get a new one!
                        if (RandomValueGenerator.GeneratePercentileIntegerValue() < 100 - LearningParameters.ValueLossRate)
                            continue;

                        // ReSharper disable once InlineOutVariableDeclaration
                        PersonalValues value;

                        if(!Enum.TryParse(testInfo.CharacteristicName, true, out value))
                            continue;

                        learningCharacter.MyTraits.PersonalValues.Remove(value);
                        Console.WriteLine(@"Removed " + value);
                    }
                    else
                    {
                        //if you win the lottery you get a new one!
                        if (RandomValueGenerator.GeneratePercentileIntegerValue() < (100 - LearningParameters.ValueAcquisitionRate / learningCharacter.MyTraits.PersonalValues.Count))
                            continue;

                        // ReSharper disable once InlineOutVariableDeclaration
                        PersonalValues value;

                        if(!Enum.TryParse(testInfo.CharacteristicName, true, out value))
                            continue;

                        var newValue = _personalValueAssociations.GetAssociatedValue(value);

                        if (newValue == null)
                            continue;

                        if (!learningCharacter.MyTraits.PersonalValues.Contains(newValue.Value))
                        {
                            learningCharacter.MyTraits.PersonalValues.Add(newValue.Value);
                            Console.WriteLine(@"Added " + value);
                        }
                    }
                }
                else
                {
                    if (testInfos.Count() < LearningParameters.NRVAThreshold)
                        continue;

                    if (RandomValueGenerator.GeneratePercentileIntegerValue() > LearningParameters.NRVARate)
                        continue;

                    // ReSharper disable once InlineOutVariableDeclaration
                    PersonalValues value;

                    Enum.TryParse(testInfo.CharacteristicName, true, out value);
                    Console.WriteLine(@"Added " + value);
                    learningCharacter.MyTraits.PersonalValues.Add(value);
                }
            }

            return true;
        }
    }
}