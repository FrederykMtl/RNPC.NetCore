using System;
using System.IO;
using System.Linq;
using RNPC.API;
using RNPC.Core.Enums;
using RNPC.Core.GameTime;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.FileManager;

namespace RNPC.Tests.Functional
{
    public abstract class AbstractFunctionalTest
    {
        protected AbstractFunctionalTest()
        {
            ConfigurationDirectory.Instance.NodeSubstitutionsFile = @"..\\..\\..\\..\\RNPC.Core\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            ConfigurationDirectory.Instance.CentralDecisionTreeRepository = @"..\\..\\..\\..\\RNPC.API\\XMLTreeFiles\\";
            ConfigurationDirectory.Instance.SubTreeRepository = @"..\\..\\..\\..\\RNPC\\Subtrees\\";
            ConfigurationDirectory.Instance.CharacterFilesDirectory = @"C:\Sysdev\RNPC\logs\Characters\";
            ConfigurationDirectory.Instance.KnowledgeFilesDirectory = @"C:\Sysdev\RNPC\Knowledge\";
            ConfigurationDirectory.Instance.LogFilesDirectory = @"C:\Sysdev\RNPC\logs\";
        }

        /// <summary>
        /// Verifies that all the needed directories exist and creates them if they don't
        /// Also copies character decision tree files
        /// </summary>
        /// <param name="characterName"></param>
        /// <param name="resetDecisionTrees"></param>
        protected void VerifyTestFilesAndDirectory(string characterName, bool resetDecisionTrees =false)
        {
            if (!Directory.Exists(ConfigurationDirectory.Instance.LogFilesDirectory))
            {
                Directory.CreateDirectory(ConfigurationDirectory.Instance.LogFilesDirectory);
            }

            if (!Directory.Exists(ConfigurationDirectory.Instance.CharacterFilesDirectory))
            {
                Directory.CreateDirectory(ConfigurationDirectory.Instance.CharacterFilesDirectory);
            }

            if (!Directory.Exists(ConfigurationDirectory.Instance.CharacterFilesDirectory))
            {
                Directory.CreateDirectory(ConfigurationDirectory.Instance.CharacterFilesDirectory);
            }

            if (!Directory.Exists(ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName))
            {
                Directory.CreateDirectory(ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName);
            }

            string characterDecisionTreesPath = ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName + "\\decisiontrees\\";

            if (!Directory.Exists(characterDecisionTreesPath))
            {
                Directory.CreateDirectory(characterDecisionTreesPath);
            }

            //if there are no decision trees we will copy them
            if (Directory.GetFiles(characterDecisionTreesPath).Length == 0 || resetDecisionTrees)
            {
                CopyFilesToDirectory(characterDecisionTreesPath, true);
            }

        }

        /// <summary>
        /// Copies decision trees to the character's directory. Directory must be created.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="overwriteFiles"></param>
        protected void SetDecisionTreeFilesForCharacter(global::RNPC.Core.Character character, bool overwriteFiles)
        {
            string characterDecisionTreesPath = ConfigurationDirectory.Instance.CharacterFilesDirectory + character.MyName + "\\decisiontrees\\";

            CopyFilesToDirectory(characterDecisionTreesPath, overwriteFiles);
        }

        private static void CopyFilesToDirectory(string path, bool overwrite)
        {
            foreach (var treeFile in Directory.EnumerateFiles(ConfigurationDirectory.Instance.CentralDecisionTreeRepository))
            {
                string fileName = Path.GetFileName(treeFile);

                if (!string.IsNullOrEmpty(fileName))
                {
                    File.Copy(treeFile, path + fileName, overwrite);
                }
            }
        }

        protected static int GetNumberOfRecentlyChangedDecisionTrees(string characterName)
        {
            string characterDecisionTreesPath = ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName + "\\decisiontrees\\";

            return Directory.GetFiles(characterDecisionTreesPath).Count(file => (DateTime.Now - File.GetLastWriteTime(file)).Minutes <= 3);
        }

        protected global::RNPC.Core.Character GetCharacterByArchetype(Archetype chosenArchetype)
        {
            switch (chosenArchetype)
            {
                case Archetype.TheInnocent:
                    return GetMeMorty();
                case Archetype.TheOrphan:
                    return GetMeAnnie();
                case Archetype.TheWarrior:
                    return GetMeThalia();
                case Archetype.TheCaregiver:
                    return GetMeJess();
                case Archetype.TheSeeker:
                    return GetMeRichardRahl();
                case Archetype.TheLover:
                    return GetMeCasanova();
                case Archetype.TheDestroyer:
                    return GetMeDrax();
                case Archetype.TheCreator:
                    return GetMeRick();
                case Archetype.TheRuler:
                    return GetMeArthur();
                case Archetype.TheMagician:
                    return GetMeHoudini();
                case Archetype.TheSage:
                    return GetMeDeckardCain();
                case Archetype.TheJester:
                    return GetMePeterGriffin();
                case Archetype.None:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(chosenArchetype), chosenArchetype, null);
            }
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
