using System.Collections.Generic;
using System.Linq;
using RNPC.API.DecisionLeaves.AnswerTextResources;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionLeaves
{
    internal class AnswerWhatIsLove : IDecisionNode
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
            if (ImARomantic(traits))
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Tone = Tone.Passionate,
                        Target = perceivedEvent.Source,
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        InitialEvent = perceivedEvent,
                        EventType = EventType.Interaction,
                        ReactionScore = 0,
                        EventName = "RomanticPoem",
                        Message = WhatIsLove.Romantic
                    }
                };
            }

            if (ImAnErudite(traits))
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Tone = Tone.Conversational,
                        Target = perceivedEvent.Source,
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        InitialEvent = perceivedEvent,
                        EventType = EventType.Interaction,
                        ReactionScore = 0,
                        EventName = "ScientificDescription",
                        Message = WhatIsLove.Scientific
                    }
                };
            }

            if (ImFunny(traits))
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Tone = Tone.Playful,
                        Target = perceivedEvent.Source,
                        Intent = Intent.Neutral,
                        ActionType = ActionType.NonVerbal,
                        InitialEvent = perceivedEvent,
                        EventType = EventType.Interaction,
                        ReactionScore = 0,
                        EventName = "ClearThroat",
                        Message = $"{memory.Me.Name} clears their throat.",
                        IntervalToNextReaction = 1
                    },
                    new Reaction
                    {
                        Tone = Tone.Playful,
                        Target = perceivedEvent.Source,
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        InitialEvent = perceivedEvent,
                        EventType = EventType.Interaction,
                        ReactionScore = 0,
                        EventName = "SingingASong",
                        Message = WhatIsLove.Song
                    }
                };
            }

            if(ImALover(memory))
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Tone = Tone.Pensive,
                        Target = perceivedEvent.Source,
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        InitialEvent = perceivedEvent,
                        EventType = EventType.Interaction,
                        ReactionScore = 0,
                        EventName = "IHaveBeenInLove",
                        Message = WhatIsLove.Lover
                    }
                };
            }

            return new List<Reaction>
            {
                new Reaction
                {
                    Tone = Tone.Hopeless,
                    Target = perceivedEvent.Source,
                    Intent = Intent.Neutral,
                    ActionType = ActionType.Verbal,
                    InitialEvent = perceivedEvent,
                    EventType = EventType.Interaction,
                    ReactionScore = 0,
                    EventName = "SadStatement",
                    Message = WhatIsLove.SadAnswer
                }
            };

        }

        private static bool ImALover(Memory memory)
        {
            return memory.Me.FindRelationshipsByType(PersonalRelationshipType.Romantic).Any();
        }

        private static bool ImARomantic(CharacterTraits traits)
        {
            return traits.PersonalValues.Contains(PersonalValues.Love);
        }

        private static bool ImAnErudite(CharacterTraits traits)
        {
            return traits.PersonalValues.Contains(PersonalValues.Knowledge);
        }

        private static bool ImFunny(CharacterTraits traits)
        {
            return traits.PersonalValues.Contains(PersonalValues.Humour);
        }

        ///<inheritdoc/>
        public string GetNodeType()
        {
            return "Leaf";
        }
    }
}