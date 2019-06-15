using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class IsSomethingWrong : IDecisionNode
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
                    Target = perceivedEvent.Source,
                    Intent = Intent.Friendly,
                    ActionType = ActionType.Verbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "IsSomethingWrong",
                    Message = $"Is something wrong? You seem off.",
                    Source = perceivedEvent.Target,
                    AssociatedKarma = 0,
                    Tone = Tone.Worried
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