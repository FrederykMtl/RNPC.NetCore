using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class IsItAWhichQuestion : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return ((Action)perceivedEvent).Message.ToLower().StartsWith("which") || 
                   ((Action)perceivedEvent).Message.ToLower().StartsWith("do you know which");
        }
    }
}