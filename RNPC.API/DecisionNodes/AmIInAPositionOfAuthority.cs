using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    // ReSharper disable once InconsistentNaming
    internal class AmIInAPositionOfAuthority : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            //Automatic Success
            if (traits.Assertiveness >= ConfiguredPassFailValue)
            {
                return TestAttributeGreaterOrEqualThanSetValue(traits.Adaptiveness, ConfiguredPassFailValue, "AutomaticSuccess", Qualities.Adaptiveness.ToString(), CharacteristicType.Quality);
            }

            Place somewhereILead = memory.Me.GetCurrentOccupation()?.FindLinkedPlaceByType(OccupationalTieType.Led);

            if (somewhereILead == null)
                return false;

            //TODO: evaluate if current location is part of a larger location i lead
            //TODO: check if he's the leader of an organization that the source is member of
            return memory.Me.GetCurrentOccupation().FindLinkedPlaceByType(OccupationalTieType.Led) == memory.MyCurrentLocation;
        }
    }
}