using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIConfrontationalOrStubborn : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic failure : prone to anger
            if (traits.Quietude <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Quietude, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Quietude.ToString());
            }

            //Automatic failure : unyielding
            if (traits.Adaptiveness <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Adaptiveness, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Adaptiveness.ToString());
            }

            //Automatic failure : too proud to back down
            if (traits.Modesty <= ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Modesty, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Modesty.ToString());
            }

            //if willpower is higher than a random value, I'm being too stubborn to back down
            return TestAttributeAgainstRandomValue(traits.Willpower, string.Empty, Qualities.Willpower.ToString());           
        }
    }
}