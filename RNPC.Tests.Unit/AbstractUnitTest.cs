using System;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.GameTime;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.FileManager;

namespace RNPC.Tests.Unit
{
    public abstract class AbstractUnitTest
    {
        protected AbstractUnitTest()
        {
            ConfigurationDirectory.Instance.NodeSubstitutionsFile = "..\\..\\..\\..\\Core\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            ConfigurationDirectory.Instance.CentralDecisionTreeRepository = "..\\..\\..\\..\\RNPC\\XMLTreeFiles\\";
            ConfigurationDirectory.Instance.SubTreeRepository = "..\\..\\..\\..\\RNPC\\Subtrees\\";
            ConfigurationDirectory.Instance.CharacterFilesDirectory = "C:\\Sysdev\\RNPC\\logs\\Characters\\";
            ConfigurationDirectory.Instance.KnowledgeFilesDirectory = "C:\\Sysdev\\RNPC\\Knowledge\\";
            ConfigurationDirectory.Instance.LogFilesDirectory = "C:\\Sysdev\\RNPC\\logs\\";
        }

        /// <summary>
        /// A random archetype
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeSterlingArcher()
        {
            // ReSharper disable once InconsistentNaming
            var Sterling = new Person("Sterling Archer", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid())
            {
                DateOfBirth = new StandardDateTime(DateTime.Now.AddYears(-33))
            };

            return new global::RNPC.Core.Character(Sterling, Archetype.None)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Morty, The Innocent
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeMorty()
        {
            // ReSharper disable once InconsistentNaming
            var Morty = new Person("Morty", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid())
            {
                DateOfBirth = new StandardDateTime(DateTime.Now.AddYears(-13))
            };

            return new global::RNPC.Core.Character(Morty, Archetype.TheInnocent)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Jessica Day, the caregiver
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeJess()
        {
            // ReSharper disable once InconsistentNaming
            var JessicaDay = new Person("Jess", Gender.Female, Sex.Female, Orientation.Straight, Guid.NewGuid())
                {
                    DateOfBirth = new StandardDateTime(DateTime.Now.AddYears(-25))
                };

            return new global::RNPC.Core.Character(JessicaDay, Archetype.TheCaregiver)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Rick The Creator
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeRick()
        {
            // ReSharper disable once InconsistentNaming
            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            return new global::RNPC.Core.Character(Rick, Archetype.TheCreator)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Drax The Destroyer
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeDrax()
        {
            // ReSharper disable once InconsistentNaming
            var Drax = new Person("Drax", Gender.Agender, Sex.Undefined, Orientation.Asexual, Guid.NewGuid());

            return new global::RNPC.Core.Character(Drax, Archetype.TheDestroyer)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Peter Griffin The Jester
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMePeterGriffin()
        {
            // ReSharper disable once InconsistentNaming
            var PeterGriffin = new Person("Peter", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            return new global::RNPC.Core.Character(PeterGriffin, Archetype.TheJester)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Casanova The Lover
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeCasanova()
        {
            // ReSharper disable once InconsistentNaming
            var Casanova = new Person("Casanova", Gender.Male, Sex.Male, Orientation.Bisexual, Guid.NewGuid());

            return new global::RNPC.Core.Character(Casanova, Archetype.TheLover)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Houdini The Magician
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeHoudini()
        {
            // ReSharper disable once InconsistentNaming
            var HarryHoudini = new Person("Houdini", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            return new global::RNPC.Core.Character(HarryHoudini, Archetype.TheMagician)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Annie The Orphan
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeAnnie()
        {
            // ReSharper disable once InconsistentNaming
            var Annie = new Person("Annie", Gender.Female, Sex.Female, Orientation.Undefined, Guid.NewGuid());

            return new global::RNPC.Core.Character(Annie, Archetype.TheOrphan)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Arthur The Ruler
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeArthur()
        {
            // ReSharper disable once InconsistentNaming
            var Arthur = new Person("Arthur", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            return new global::RNPC.Core.Character(Arthur, Archetype.TheRuler)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Deckard Cain The Sage
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeDeckardCain()
        {
            // ReSharper disable once InconsistentNaming
            var DeckardCain = new Person("Deckard", Gender.Male, Sex.Male, Orientation.Undefined, Guid.NewGuid());

            return new global::RNPC.Core.Character(DeckardCain, Archetype.TheSage)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Richard The Seeker
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeRichardRahl()
        {
            // ReSharper disable once InconsistentNaming
            var RichardRahl = new Person("Richard", Gender.Male, Sex.Male, Orientation.Straight, Guid.NewGuid());

            return new global::RNPC.Core.Character(RichardRahl, Archetype.TheSeeker)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }

        /// <summary>
        /// Thalia The Warrior
        /// </summary>
        /// <returns></returns>
        protected global::RNPC.Core.Character GetMeThalia()
        {
            // ReSharper disable once InconsistentNaming
            var ThaliaOfThraben = new Person("Thalia", Gender.Female, Sex.Female, Orientation.Undefined, Guid.NewGuid());

            return new global::RNPC.Core.Character(ThaliaOfThraben, Archetype.TheWarrior)
            {
                FileController = new DecisionTreeFileController(),
                DecisionTreeBuilder = new DecisionTreeBuilder()
            };
        }
    }
}