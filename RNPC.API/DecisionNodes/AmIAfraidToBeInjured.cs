using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIAfraidToBeInjured : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //If strength or boldness are part of my values I will not be afraid of injury
            if (CharacterHasPersonalValue(PersonalValues.Strength, traits) || CharacterHasPersonalValue(PersonalValues.Boldness, traits))
                return false;

            //If my security is important I have a 20% penalty
            int penalty =  - (CharacterHasPersonalValue(PersonalValues.Security, traits) ? 20: 0);

            return TestAttributeAgainstRandomValue(traits.Confidence, penalty!=0 ? "Penalty(Security)" : string.Empty, Qualities.Confidence.ToString(), penalty);
        }
    }
}