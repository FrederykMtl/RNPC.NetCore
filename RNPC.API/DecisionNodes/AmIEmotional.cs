using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIEmotional : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {   //TODO: rethink
            //Emotional, sad.
            var myTestResult = new NodeTestInfo
            {
                TestedCharacteristic = CharacteristicType.Emotion,
                CharacteristicName = "Sadness",
                AttributeValue = traits.ShortTermEmotions.Sadness,
                PassingValue = 25,
                Result = traits.ShortTermEmotions.Sadness >= 25, //this is the test
                Description = string.Empty
            };

            if (myTestResult.Result)
            {
                DataToMemorize.Add(myTestResult);
                return true;
            }

            if (traits.Selflessness > traits.Compassion)
                //Automatic failure: not enough compassion
                if (traits.Compassion <= ConfiguredPassFailValue)
                {
                    return TestAttributeSmallerOrEqualThanSetValue(traits.Compassion, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Compassion.ToString());
                }
            else
                //Automatic failure: too selfish
                if (traits.Selflessness <= ConfiguredPassFailValue)
                {
                    return TestAttributeSmallerOrEqualThanSetValue(traits.Selflessness, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Selflessness.ToString());
                }

            return true;
        }
    }
}