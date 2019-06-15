using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIGoingToReactViolently : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic failure
            if (traits.Willpower < ConfiguredPassFailValue)
            {
                return TestAttributeSmallerOrEqualThanSetValue(traits.Willpower, ConfiguredPassFailValue, "AutomaticFailure", Qualities.Willpower.ToString());
            }

            //probabilistic failure : 15% chance
            if (CharacterHasPersonalValue(PersonalValues.Peace, traits) ||
                CharacterHasPersonalValue(PersonalValues.Security, traits) ||
                CharacterHasPersonalValue(PersonalValues.InnerHarmony, traits))
            {
                return !SetValueIsGreaterThanRandomValue(15);
            }

            return TestAttributeSmallerOrEqualThanSetValue(traits.Quietude, ConfiguredPassFailValue, "Conditional on:EVAL(Willpower,RND))", Qualities.Quietude.ToString()) &&
                   TestAttributeAgainstRandomValue(traits.Willpower, $"Conditional on:EVAL(Quietude,{ConfiguredPassFailValue})", Qualities.Willpower.ToString());
        }
    }
}