using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class QuestionStructureIsWhoIs : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return ((Action) perceivedEvent).Message.ToLower().Contains("who is");
        }
    }
}
