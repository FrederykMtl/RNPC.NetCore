using System.Collections.Generic;
using RNPC.Core.Action;
using RNPC.Core.Interfaces;

namespace RNPC.Core.ContextAnalysis
{
    public interface IContextStrategy
    {
        List<Reaction> Evaluate(Character character, Action.Action action, IDecisionNode decisionTreeRootNode);
    }
}