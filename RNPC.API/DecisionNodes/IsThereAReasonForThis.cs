using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class IsThereAReasonForThis : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var sourcePerson = memory.Persons.FindPersonByName(perceivedEvent.Source);

            if (sourcePerson == null)
                return false;

            var conflict = memory.Events.FindEventsByTypeAndRelatedPerson(PastEventType.Conflict, sourcePerson);

            return conflict != null;
        }
    }
}