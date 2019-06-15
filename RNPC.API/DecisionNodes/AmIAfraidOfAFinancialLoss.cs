using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIAfraidOfAFinancialLoss : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Is money or (financial) security important to me?
            if (!CharacterHasPersonalValue(PersonalValues.Wealth, traits) && !CharacterHasPersonalValue(PersonalValues.Security, traits))
                return false;

            return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Value(Wealth,Security)", Qualities.Confidence.ToString());
        }
    }
}