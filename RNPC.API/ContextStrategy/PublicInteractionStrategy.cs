using System.Collections.Generic;
using System.Linq;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.ContextAnalysis;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Interfaces;

namespace RNPC.API.ContextStrategy
{
    public class PublicInteractionStrategy : IContextStrategy
    {
        public List<Reaction> Evaluate(Character character, Action action, IDecisionNode decisionTreeRootNode)
        {
            var reaction = decisionTreeRootNode.Evaluate(character.MyTraits, character.MyMemory, action);

            character.MyMemory.AddNodeTestResults(((AbstractDecisionNode)decisionTreeRootNode).GetNodeTestsData());

            reaction[0].ReactionScore = ((AbstractDecisionNode)decisionTreeRootNode).GetNodeTestsData().Sum(info => info.ProfileScore);

            character.MyMemory.AddRecentReactions(reaction);

            return reaction;
        }
    }
}