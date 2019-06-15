using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class RudeGesture : IDecisionNode
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
                    Intent = Intent.Hostile,
                    ActionType = ActionType.NonVerbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "RudeGesture",
                    Message = GetRudeGestureMessage(perceivedEvent, memory),
                    Source = perceivedEvent.Target,
                    AssociatedKarma = -1
                }
            };
        }

        private static string GetRudeGestureMessage(PerceivedEvent perceivedEvent, Memory memory)
        {
            if(memory.Me.Age(Cronos.Instance.GetCurrentTime()) <= 6)
                return $"{perceivedEvent.Target} makes a grimace at {perceivedEvent.Source}";

            return $"{perceivedEvent.Target} raises his middle finger to {perceivedEvent.Source}";
        }

        ///<inheritdoc/>
        public string GetNodeType()
        {
            return "Leaf";
        }
    }
}