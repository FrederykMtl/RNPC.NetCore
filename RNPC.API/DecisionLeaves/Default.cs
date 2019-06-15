using System;
using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class Default : IDecisionNode
    {
        public IDecisionNode ParentNode { get; private set; }

        public void SetParentNode(IDecisionNode parent)
        {
            ParentNode = parent;
        }

        /// <summary>
        /// Returns the parent node
        /// </summary>
        /// <returns></returns>
        public IDecisionNode GetParentNode()
        {
            return ParentNode;
        }

        public List<Reaction> Evaluate(CharacterTraits traits, Memory memory, PerceivedEvent perceivedEvent)
        {
            throw new Exception("Default class is a placeholder. It should not be used.");
        }

        ///<inheritdoc/>
        public string GetNodeType()
        {
            return "Leaf";
        }
    }
}