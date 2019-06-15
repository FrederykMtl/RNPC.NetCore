using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class AnnoyedWhoAreYou : IDecisionNode
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
                    Tone = Tone.Annoyed,
                    Target = perceivedEvent.Source,
                    Intent = Intent.Hostile,
                    ActionType = ActionType.NonVerbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "Pause",
                    Message = $"{memory.Me.Name} pauses.",
                    AssociatedKarma = 0,
                    IntervalToNextReaction = 5
                },
                new Reaction
                {
                    Tone = Tone.Annoyed,
                    Target = perceivedEvent.Source,
                    Intent = Intent.Hostile,
                    ActionType = ActionType.Verbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "WhoAreYou",
                    Message = "And who are you?",
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