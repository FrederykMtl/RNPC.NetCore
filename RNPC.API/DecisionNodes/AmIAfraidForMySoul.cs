using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIAfraidForMySoul : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            if (!CharacterHasPersonalValue(PersonalValues.Faith, traits))
                return false;

            if (memory.HaveIDoneSomethingUnforgivable())
            {
                var unforgivenTestResult = new NodeTestInfo
                {
                    TestedCharacteristic = CharacteristicType.Memory,
                    CharacteristicName = "PastActions",
                    Result = true,
                    Description = "Conditional on:Value(Faith)"
                };
                DataToMemorize.Add(unforgivenTestResult);

                return true;
            }

            var myTestResult = new NodeTestInfo
            {
                TestedCharacteristic = CharacteristicType.Karma,
                CharacteristicName = "Karma",
                AttributeValue = memory.MyKarmaScore,
                PassingValue = -5,
                Result = memory.MyKarmaScore < -5, //this is the test
                Description = "Conditional on:Value(Faith)"
            };
            DataToMemorize.Add(myTestResult);

            return myTestResult.Result;

        }
    }
}