using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class DoIKnowThisPerson : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var person = memory.Persons.FindPersonByName(perceivedEvent.Source);

            return person != null;
        }
    }
}