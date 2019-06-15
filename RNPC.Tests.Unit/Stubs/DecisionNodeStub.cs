using System.Collections.Generic;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;

namespace RNPC.Tests.Unit.Stubs
{
    public class DecisionNodeStub : IDecisionNode
    {
        // ReSharper disable once NotAccessedField.Local
        private IDecisionNode _parentNode;

        /// <summary>
        /// Returns the parent node
        /// </summary>
        /// <returns></returns>
        public IDecisionNode GetParentNode()
        {
            return _parentNode;
        }

        public List<Reaction> Evaluate(CharacterTraits traits, global::RNPC.Core.Memory.Memory memory, PerceivedEvent perceivedEvent)
        {
            return new List<Reaction>
            {
                new Reaction
                {
                    Tone = Tone.Neutral,
                    Target = perceivedEvent.Source,
                    Intent = Intent.Neutral,
                    ActionType = ActionType.NonVerbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "TestStub",
                    Message = "Test Complete",
                    AssociatedKarma = 0
                }
            };
        }

        public string GetNodeType()
        {
            return "Stub Node";
        }

        public void SetParentNode(IDecisionNode parent)
        {
            _parentNode = parent;
        }
    }
}
