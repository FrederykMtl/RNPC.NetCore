using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class EnquireAboutMotive : IDecisionNode
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
                    Tone = Tone.Empathetic,
                    Target = perceivedEvent.Source,
                    Intent = Intent.Friendly,
                    ActionType = ActionType.Verbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "Question",
                    Message = "Why are you lashing out like this?" //TODO: Randomize
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