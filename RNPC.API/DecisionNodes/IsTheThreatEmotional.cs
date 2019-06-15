using System;
using System.Collections;
using System.Globalization;
using RNPC.API.TextResources;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Exceptions;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;

namespace RNPC.API.DecisionNodes
{
    internal class IsTheThreatEmotional : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            Action perceivedAction = perceivedEvent as Action;

            if(perceivedAction == null)
                throw new RnpcParameterException("Perceived events that are not of Action type should not be passed to social interactions", new Exception("Incorrect parameter type passed in class " + this));

            return IsItAnEmotionalThreat(perceivedAction.Message);	
        }

        private static bool IsItAnEmotionalThreat(string threat)
        {
            var resourceSet = EmotionalThreatKeywords.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (threat.Contains(entry.Value.ToString()))
                    return true;
            }

            return false;
        }
    }
}