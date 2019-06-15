using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Memory;

namespace RNPC.API.DecisionNodes
{
    internal class CanILaughAtMyself : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            int bonus = CharacterHasPersonalValue(PersonalValues.Humour, traits) ? 10 : 0;

	    	return TestAttributeAgainstRandomValue(traits.Modesty, bonus!=0? "Bonus(10)": string.Empty, Qualities.Modesty.ToString(), bonus);	
        }
    }
}