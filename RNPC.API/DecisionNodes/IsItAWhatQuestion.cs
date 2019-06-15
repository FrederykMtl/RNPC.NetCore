using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class IsItAWhatQuestion : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            return ((Action)perceivedEvent).Message.ToLower().StartsWith("wato") || 
                   ((Action)perceivedEvent).Message.ToLower().StartsWith("do you know what");
        }
    }
}