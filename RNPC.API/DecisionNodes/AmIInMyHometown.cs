using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class AmIInMyHometown : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var hometown = memory.Me.FindPlaceByPersonalTieType(PersonalTieType.OriginatedFrom);

            return (hometown != null && hometown == memory.MyCurrentLocation);
        }
    }
}