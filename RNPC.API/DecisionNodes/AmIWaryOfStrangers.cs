using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIWaryOfStrangers : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            Place whereIComeFrom = memory.Me.FindPlaceByPersonalTieType(PersonalTieType.LiveIn);

            if (whereIComeFrom == null)
                return false;

            return whereIComeFrom.IsThisASmallTown() && TestAttributeAgainstRandomValue(traits.Gregariousness, string.Empty, Qualities.Gregariousness.ToString());
        }
    }
}