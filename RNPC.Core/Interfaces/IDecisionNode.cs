using System.Collections.Generic;
using RNPC.Core.Action;

namespace RNPC.Core.Interfaces
{
    public interface IDecisionNode
    {
        List<Reaction> Evaluate(CharacterTraits traits, Memory.Memory memory, PerceivedEvent perceivedEvent);
        /// <summary>
        /// Indicates the node type
        /// </summary>
        /// <returns>The node type</returns>
        string GetNodeType();
        void SetParentNode(IDecisionNode parent);
        IDecisionNode GetParentNode();
    }
}