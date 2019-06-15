using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class IsItAWhenQuestion : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return ((Action)perceivedEvent).Message.ToLower().StartsWith("when") || 
                   ((Action)perceivedEvent).Message.ToLower().StartsWith("do you know when");
        }
    }
}