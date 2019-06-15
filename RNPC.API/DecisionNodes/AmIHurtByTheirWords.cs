using System;
using System.Collections;
using System.Globalization;
using RNPC.API.TextResources;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;

namespace RNPC.API.DecisionNodes
{
    internal class AmIHurtByTheirWords : AbstractDecisionNode
    {
        protected override bool EvaluateNode(PerceivedEvent perceivedEvent, Memory memory, CharacterTraits traits)
        {
            var insult = perceivedEvent as Action;

            if (insult == null)
                throw new RnpcParameterException("Perceived events that are not of Action type should not be passed to social interactions", new Exception("Incorrect parameter type passed in class " + this));

            if (InsultIsAboutMyIntelligence(insult.Message))
            {
                //if intelligence or knowledge are important to  me AND my self-esteem isn't high enough I'll be hurt
                if (CharacterHasPersonalValue(PersonalValues.Knowledge, traits) || CharacterHasPersonalValue(PersonalValues.Curiosity, traits))
                    return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Value(Knowledge,Curiosity)", Qualities.Confidence.ToString());

                //if I'm not very smart (quick witted) AND my self-esteem isn't high enough I'll be hurt
                if (TestAttributeSmallerOrEqualThanSetValue(traits.Acuity, 50, "ALT to: Value(Knowledge, Curiosity)", Qualities.Acuity.ToString()))
                    return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:EVAL(Acuity,50)", Qualities.Confidence.ToString());

                return false;
            }

            if (InsultIsAboutMyCourage(insult.Message))
            {
                //if boldness or challenging myself are important to  me OR I'm not very determined AND my self-esteem isn't high enough I'll be hurt
                 if(CharacterHasPersonalValue(PersonalValues.Boldness, traits) || CharacterHasPersonalValue(PersonalValues.Challenge, traits))
                     return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Value(Boldness,Challenge)", Qualities.Confidence.ToString());

                if(TestAttributeSmallerOrEqualThanSetValue(traits.Determination, 50, "ALT to:Value(Boldness,Challenge)", Qualities.Determination.ToString()))
                   return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:EVAL(Determination,50)", Qualities.Confidence.ToString());

                return false;
            }

            if (InsultIsAboutMyStrength(insult.Message))
            {
                //if strength or autonomy are important to me AND my self-esteem isn't high enough I'll be hurt
                if (CharacterHasPersonalValue(PersonalValues.Strength, traits) || CharacterHasPersonalValue(PersonalValues.Autonomy, traits))
                       return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Value(Strength,Autonomy)", Qualities.Confidence.ToString());

                return false;
            }

            if (InsultIsAboutMyFamily(insult.Message))
            {
                //if my family (community) or respecting others are important to me I'll be hurt / angry
                return CharacterHasPersonalValue(PersonalValues.Community, traits) || CharacterHasPersonalValue(PersonalValues.Respect, traits);
            }

            if (InsultIsAboutMyHonour(insult.Message))
            {
                //if my honour or how others see me are important to me AND my self-esteem isn't high enough I'll be hurt
                if(CharacterHasPersonalValue(PersonalValues.Honour, traits) || CharacterHasPersonalValue(PersonalValues.Status, traits))
                    return TestAttributeAgainstRandomValue(traits.Confidence, "Conditional on:Value(Honour,Status)", Qualities.Confidence.ToString());

                return false;
            }

            //Otherwise we only check their self-esteem
            return TestAttributeAgainstRandomValue(traits.Confidence, "Unique", Qualities.Confidence.ToString());
        }

        /// <summary>
        /// Analyzes the content of the insult to determine its nature, based on specific keywords
        /// Keyword file : IntelligenceThreatKeywords.resx
        /// </summary>
        /// <param name="mockeryMessage">insult text</param>
        /// <returns>Whether the insult attacks the target's intelligence</returns>
        private static bool InsultIsAboutMyIntelligence(string mockeryMessage)
        {
            var resourceSet = IntelligenceInsultKeywords.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, false);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (mockeryMessage.ToLower().Contains(entry.Value.ToString()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Analyzes the content of the insult to determine its nature, based on specific keywords
        /// Keyword file : CourageThreatKeywords.resx
        /// </summary>
        /// <param name="mockeryMessage">insult text</param>
        /// <returns>Whether the insult attacks the target's courage</returns>
        private static bool InsultIsAboutMyCourage(string mockeryMessage)
        {
            var resourceSet = CourageInsultKeywords.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, false);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (mockeryMessage.Contains(entry.Value.ToString()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Analyzes the content of the insult to determine its nature, based on specific keywords
        /// Keyword file : StrengthThreatKeywords.resx
        /// </summary>
        /// <param name="mockeryMessage">insult text</param>
        /// <returns>Whether the insult attacks the target's strength</returns>
        private static bool InsultIsAboutMyStrength(string mockeryMessage)
        {
            var resourceSet = IntelligenceInsultKeywords.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, false);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (mockeryMessage.Contains(entry.Value.ToString()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Analyzes the content of the insult to determine its nature, based on specific keywords
        /// Keyword file : FamilyThreatKeywords.resx
        /// </summary>
        /// <param name="mockeryMessage">insult text</param>
        /// <returns>Whether the insult attacks the target's family</returns>
        private static bool InsultIsAboutMyFamily(string mockeryMessage)
        {
            var resourceSet = FamilyInsultKeywords.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, false);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (mockeryMessage.Contains(entry.Value.ToString()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Analyzes the content of the insult to determine its nature, based on specific keywords
        /// Keyword file : HonourThreatKeywords.resx
        /// </summary>
        /// <param name="mockeryMessage">insult text</param>
        /// <returns>Whether the insult attacks the target's honour</returns>
        private bool InsultIsAboutMyHonour(string mockeryMessage)
        {
            var resourceSet = HonourInsultKeywords.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, false);

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (mockeryMessage.Contains(entry.Value.ToString()))
                    return true;
            }

            return false;
        }
    }
}