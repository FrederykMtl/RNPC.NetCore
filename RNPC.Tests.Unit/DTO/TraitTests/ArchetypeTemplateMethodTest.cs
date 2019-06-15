using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core;
using RNPC.Core.Enums;
using RNPC.Core.InitializationStrategies;
using RNPC.Core.Resources;
using RNPC.Core.TraitRules;

namespace RNPC.Tests.Unit.DTO.TraitTests
{
    [TestClass]
    public class ArchetypeTemplateMethodTest
    {
        [TestMethod]
        public void AttributesCheck_TheCaregiver_AttributesCorrespondToArchetype()
        {
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Florence Nightingale", Sex.Female);

                new TheCaregiverInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Compassion >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Selflessness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Tolerance >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Introspection <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.SelfEsteem <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Service));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Compassion));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Community));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheCreator_AttributesCorrespondToArchetype()
        {
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Thomas Edison", Sex.Male);

                new TheCreatorInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Inventiveness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Imagination >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Ambition >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Modesty <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Introspection <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Achievement));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Creativity));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Growth));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheDestroyer_AttributesCorrespondToArchetype()
        {
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Che Guevara", Sex.Male);

                new TheDestroyerInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Modesty >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Tolerance >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Changing >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Compassion <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Conscience <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Justice));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Community));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Growth));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheInnocent_AttributesCorrespondToArchetype()
        {
            //Outlook, Tolerance, Gregariousness	Critical sense, Willpower	Honesty, Stability, Security
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Rachel Green", Sex.Female);

                new TheInnocentInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Outlook >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Tolerance >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Gregariousness >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.CriticalSense <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Willpower <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Honesty));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Stability));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Security));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheJester_AttributesCorrespondToArchetype()
        {
            //Acuity, Expressiveness, Inventiveness	Conscience, Selflessness	Curiosity, Humour, Pleasure
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Loki", Sex.Male);

                new TheJesterInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Acuity >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Expressiveness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Inventiveness >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Conscience <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Selflessness <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Curiosity));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Humour));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Pleasure));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheLover_AttributesCorrespondToArchetype()
        {
            //Sociability, Expressiveness, Charisma	Ambition, Willpower	Love, Pleasure, Happiness
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Giovanni Casanova", Sex.Male);

                new TheLoverInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Sociability >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Expressiveness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Charisma >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Ambition <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Willpower <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Love));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Pleasure));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Happiness));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheMagician_AttributesCorrespondToArchetype()
        {
            //Changing, Acuity, Inventiveness	Selflessness, Charitable	Knowledge, Curiosity, Learning
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Steve Jobs", Sex.Male);

                new TheMagicianInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Changing >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Acuity >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Inventiveness >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Selflessness <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Charitable <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Knowledge));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Curiosity));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Learning));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheOrphan_AttributesCorrespondToArchetype()
        {
            //Critical sense, Compassion, Determination	Outlook, Gregariousness	Independance, Autonomy, Freedom
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("James Howlett", Sex.Male);

                new TheOrphanInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.CriticalSense >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Compassion >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Determination >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Outlook <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Gregariousness <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Independance));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Autonomy));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Freedom));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheRuler_AttributesCorrespondToArchetype()
        {
            //Assertiveness, Determination, Expressiveness	Inventiveness, Imagination	Responsibility, Competency, Authority
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("King Arthur", Sex.Male);

                new TheRulerInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Assertiveness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Determination >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Expressiveness >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Inventiveness <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Imagination <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Responsibility));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Competency));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Authority));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheSage_AttributesCorrespondToArchetype()
        {
            //Critical Sense, Awareness, Memory	Outlook, Imagination	Knowledge, Wisdom, Truth
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Yoda", Sex.Male);

                new TheSageInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.CriticalSense >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Awareness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Memory >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Outlook <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Imagination <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Knowledge));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Wisdom));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Truth));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheSeeker_AttributesCorrespondToArchetype()
        {
            //Imagination, Adaptiveness, Ambition	Tolerance, Introspection	Autonomy, Adventure, Learning
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("James T. Kirk", Sex.Male);

                new TheSeekerInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Imagination >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Adaptiveness >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Ambition >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Tolerance <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Introspection <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Adventure));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Autonomy));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Learning));
            }
        }

        [TestMethod]
        public void AttributesCheck_TheWarrior_AttributesCorrespondToArchetype()
        {
            //Determination, Tolerance, Willpower	Changing, Imagination	Honour, Strength, Service
            for (int i = 0; i < 5; i++)
            {
                var traits = new CharacterTraits("Diana Prince", Sex.Female);

                new TheWarriorInitializationMethod().Initialize(ref traits, new QualityRuleEvaluator(),
                    new EmotionRuleEvaluator());

                Assert.IsTrue(traits.Determination >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Tolerance >= Constants.MinStrongPoint);
                Assert.IsTrue(traits.Willpower >= Constants.MinStrongPoint);

                Assert.IsTrue(traits.Changing <= Constants.MaxWeakPoint);
                Assert.IsTrue(traits.Imagination <= Constants.MaxWeakPoint);

                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Honour));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Strength));
                Assert.IsTrue(traits.PersonalValues.Contains(PersonalValues.Service));
            }
        }
    }
}
