using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class YoureAnIdiot : IDecisionNode
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
            return new List<Reaction>
            {
                new Reaction
                {
                    Tone = Tone.Condescending,
                    Target = perceivedEvent.Source,
                    Intent = Intent.Hostile,
                    ActionType = ActionType.Verbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "YoureAnIdiot",
                    Message = "You're an idiot.",
                    AssociatedKarma = -1
                }
            };
        }

        ///<inheritdoc/>
        public string GetNodeType()
        {
            return "Leaf";
        }
    }
}