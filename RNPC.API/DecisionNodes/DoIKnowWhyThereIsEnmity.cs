using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIKnowWhyThereIsEnmity : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var conflictEvents = memory.Events.FindEventsByTypeAndRelatedPerson(PastEventType.Conflict, perceivedEvent.Source);

            if (conflictEvents == null || conflictEvents.Count == 0)
                return false;

            //Are we still in conflict about something?
            return conflictEvents.Exists(e => e.Ended == null);
        }
    }
}