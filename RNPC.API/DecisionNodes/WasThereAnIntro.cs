using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class WasThereAnIntro : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var reaction = perceivedEvent as Reaction;

            return reaction != null && CheckConversationHistoryForIntroduction(reaction.InitialEvent);
        }

        private static bool CheckConversationHistoryForIntroduction(PerceivedEvent eventReactedTo)
        {
            if (eventReactedTo.EventType == EventType.Interaction && eventReactedTo.EventName.Contains("Introduce"))
                return true;

            var reaction = eventReactedTo as Reaction;

            return reaction != null && CheckConversationHistoryForIntroduction(reaction.InitialEvent);
        }
    }
}