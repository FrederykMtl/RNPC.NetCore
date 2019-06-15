using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Enums;
using RNPC.Core.Memory;
using Action = RNPC.Core.Action.Action;

namespace RNPC.Tests.Functional.Character
{
    [TestClass]
    public class CharacterStatisticsTest : AbstractFunctionalTest
    {
        public class ArchetypeStatistics
        {
            public string Archetype;
            public string Event;
            public double Max;
            public double Min;
            public double Average;
        }
        [Ignore]
        [TestMethod]
        public void InteractWithMe_TestMultipleArchetypesWithMultipleIterations_MinMaxAverageNumbersCompiled()
        {
            List<List<double>> results = new List<List<double>>();
            List<ArchetypeStatistics> statistics = new List<ArchetypeStatistics>();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 100; i++)
            {
                var character = GetMeRick();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheCreator));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeMorty();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheInnocent));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeJess();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheCaregiver));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeDrax();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheDestroyer));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMePeterGriffin();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheJester));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeCasanova();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheLover));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeHoudini();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheMagician));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeAnnie();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheOrphan));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeArthur();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheRuler));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeDeckardCain();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheSage));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeRichardRahl();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheSeeker));
            results.Clear();

            //Create 12 lists of results
            for (int i = 0; i < 12; i++)
            {
                results.Add(new List<double>());
            }

            for (int i = 0; i < 1000; i++)
            {
                var character = GetMeThalia();

                PutCharacterThroughTheTwelveLabours(character, results);
            }

            statistics.AddRange(CompileStatistics(results, Archetype.TheWarrior));
            results.Clear();

            string currentArchetype = statistics[0].Archetype;
            Debug.WriteLine(currentArchetype);
            foreach (var statistic in statistics)
            {
                if (statistic.Archetype != currentArchetype)
                {
                    currentArchetype = statistic.Archetype;
                    Debug.WriteLine(currentArchetype);
                }
                Debug.WriteLine($"{statistic.Event}\t{statistic.Average}\t{statistic.Max}\t{statistic.Min}");
            }

            var total = statistics.Count;
            Assert.IsTrue(total != 0);
        }

        /// <summary>
        /// Submits a character to the 12 tests used for the psychology game
        /// </summary>
        /// <param name="character"></param>
        /// <param name="compiledResults"></param>
        private static void PutCharacterThroughTheTwelveLabours(global::RNPC.Core.Character character, IReadOnlyList<List<double>> compiledResults)
        {

            //Friendly Greeting
            Action greeting = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Friendly,
                Message = "Hi!",
                Target = character.MyName,
                EventName = "Greeting",
                Source = "The Friend"
            };
            var reaction1 = character.InteractWithMe(greeting);
            compiledResults[0].Add(reaction1[0].ReactionScore);

            Action threat = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Tone = Tone.Threatening,
                Message = "I'm going to kill you!",
                Target = character.MyName,
                EventName = "Threat",
                Source = "The Enemy"
            };

            var reaction2 = character.InteractWithMe(threat);
            compiledResults[1].Add(reaction2[0].ReactionScore);

            //Neutral Greeting
            Action neutralGreeting = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "Hi",
                Target = character.MyName,
                EventName = "Greeting",
                Source = "The Ambassador"
            };

            var reaction3 = character.InteractWithMe(neutralGreeting);
            compiledResults[2].Add(reaction3[0].ReactionScore);

            //Friendly How Are You?
            Action enquire = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Friendly,
                Message = "How are you?",
                Target = character.MyName,
                EventName = "HowAreYou",
                Source = "The Friend"
            };

            var reaction4 = character.InteractWithMe(enquire);
            compiledResults[3].Add(reaction4[0].ReactionScore);

            //Hostile-Insult
            Action insult = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "You're really dumb.",
                Target = character.MyName,
                EventName = "Insult",
                Source = "The Frenemy"
            };

            var reaction5 = character.InteractWithMe(insult);
            compiledResults[4].Add(reaction5[0].ReactionScore);

            //Neutral How Are You?
            Action neutralEnquire = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "How are you?",
                Target = character.MyName,
                EventName = "HowAreYou",
                Source = "The Ambassador"
            };

            var reaction6 = character.InteractWithMe(neutralEnquire);
            compiledResults[5].Add(reaction6[0].ReactionScore);

            //Friendly Teasing
            Action teasing = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Friendly,
                Message = "Did you have a fight with your comb this morning?",
                Target = character.MyName,
                EventName = "Teasing",
                Source = "The Friend"
            };

            var reaction7 = character.InteractWithMe(teasing);
            compiledResults[6].Add(reaction7[0].ReactionScore);

            //Hostile Mockery
            Action mockery = new Action
            {
                Tone = Tone.Mocking,
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "Did your mother dress you up this morning?",
                Target = character.MyName,
                EventName = "Mocking",
                Source = "The Bully"
            };

            var reaction8 = character.InteractWithMe(mockery);
            compiledResults[7].Add(reaction8[0].ReactionScore);

            //Neutral Apology
            Action apology = new Action
            {
                Tone = Tone.Apologetic,
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Neutral,
                Message = "My apologies",
                Target = character.MyName,
                EventName = "Apology",
                Source = "The Ambassador"
            };

            var reaction9 = character.InteractWithMe(apology);
            compiledResults[8].Add(reaction9[0].ReactionScore);

            //Friendly Smile
            Action smile = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.NonVerbal,
                Intent = Intent.Friendly,
                Message = "",
                Target = character.MyName,
                EventName = "Smile",
                Source = "The Friend"
            };

            var reaction10 = character.InteractWithMe(smile);
            compiledResults[9].Add(reaction10[0].ReactionScore);

            //Hostile Glare
            Action glare = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.NonVerbal,
                Intent = Intent.Hostile,
                Message = "",
                Target = character.MyName,
                EventName = "Glare",
                Source = "The Bully"
            };

            var reaction11 = character.InteractWithMe(glare);
            compiledResults[10].Add(reaction11[0].ReactionScore);

            //Neutral Salute
            Action salute = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.NonVerbal,
                Intent = Intent.Neutral,
                Message = "",
                Target = character.MyName,
                EventName = "Salute",
                Source = "The Ambassador"
            };

            var reaction12 = character.InteractWithMe(salute);
            compiledResults[11].Add(reaction12[0].ReactionScore);
        }

        private IEnumerable<ArchetypeStatistics> CompileStatistics(List<List<double>> results, Archetype archetype)
        {
            List<ArchetypeStatistics> archetypeStatistics = new List<ArchetypeStatistics>
            {
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Friendly Greeting",
                    Min = results[0].Min(),
                    Max = results[0].Max(),
                    Average = results[0].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Hostile Threat",
                    Min = results[1].Min(),
                    Max = results[1].Max(),
                    Average = results[1].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Neutral Greeting",
                    Min = results[2].Min(),
                    Max = results[2].Max(),
                    Average = results[2].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Friendly How Are You",
                    Min = results[3].Min(),
                    Max = results[3].Max(),
                    Average = results[3].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Hostile Insult",
                    Min = results[4].Min(),
                    Max = results[4].Max(),
                    Average = results[4].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Neutral How Are You",
                    Min = results[5].Min(),
                    Max = results[5].Max(),
                    Average = results[5].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Friendly Teasing",
                    Min = results[6].Min(),
                    Max = results[6].Max(),
                    Average = results[6].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Hostile Mockery",
                    Min = results[7].Min(),
                    Max = results[7].Max(),
                    Average = results[7].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Neutral Apologyg",
                    Min = results[8].Min(),
                    Max = results[8].Max(),
                    Average = results[8].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Friendly Smile",
                    Min = results[9].Min(),
                    Max = results[9].Max(),
                    Average = results[9].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Hostile Glare",
                    Min = results[10].Min(),
                    Max = results[10].Max(),
                    Average = results[10].Average()
                },
                new ArchetypeStatistics
                {
                    Archetype = archetype.ToString(),
                    Event = "Neutral Salute",
                    Min = results[11].Min(),
                    Max = results[11].Max(),
                    Average = results[11].Average()
                }
            };

            //Friendly Greeting

            //Hostile Threat

            //Neutral Greeting

            //Friendly How Are You?

            //Hostile-Insult
            //Neutral How Are You?

            //Friendly Teasing

            //Hostile Mockery

            //Neutral Apology

            //Friendly Smile

            //Hostile Glare

            //Neutral Salute

            return archetypeStatistics;
        }

        [TestMethod]
        public double InteractWithMe_InsultACreatorArchetype_ScoreDifferentThanZero()
        {
            // ReSharper disable once InconsistentNaming
            var Rick = new Person("Rick", Gender.Male, Sex.Male, Orientation.Pansexual, Guid.NewGuid());

            //ARRANGE
            var testCharacter = new global::RNPC.Core.Character(Rick, Archetype.TheCreator);

            Action insult = new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "You're an asshole, Rick!",
                Target = "Rick",
                EventName = "Insult",
                Source = "Morty"
            };

            //ACT
            var reaction = testCharacter.InteractWithMe(insult);
            //ASSERT
            Assert.IsNotNull(reaction);
            Assert.AreEqual(reaction[0].InitialEvent, insult);
            Assert.AreEqual(reaction[0].Target, @"Morty");

            //int nbOfStrongPoints = CalculateNbOfStrongPoints(testCharacter.MyTraits);

            return reaction[0].ReactionScore;
        }
    }
}