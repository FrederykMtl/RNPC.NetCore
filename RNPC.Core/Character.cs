using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using RNPC.Core.Action;
using RNPC.Core.ContextAnalysis;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Core.InitializationStrategies;
using RNPC.Core.Interfaces;
using RNPC.Core.KnowledgeBuildingStrategies;
using RNPC.Core.Learning;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.Core.TraitGeneration;
using RNPC.Core.TraitRules;

[assembly: InternalsVisibleTo("RNPC.API")]
[assembly: InternalsVisibleTo("RNPC.Tests.Functional")]
[assembly: InternalsVisibleTo("RNPC.Tests.Unit")]

namespace RNPC.Core
{
    [Serializable]
    public class Character
    {
        private const int PointOfExhaustion = 3;
        #region Properties

        public Guid UniqueId { get; }
        public bool IsStrictlyReactive = true;
        /// <summary>
        /// Indicates that the character has gone through initial training
        /// Can ony be done once
        /// </summary>
        public bool HasBeenTrained { get; private set; }

        public string MyName => MyMemory.GetMyInformation().Name;

        [NonSerialized] public IXmlFileController FileController;
        [NonSerialized] public ITreeBuilder DecisionTreeBuilder;
        [NonSerialized] public IKnowledgeRandomizationStrategy KnowledgeRandomizer;
        [NonSerialized] public IContextAnalyzer ContextAnalyzer;
        [NonSerialized] public IContextStrategyFactory ContextStrategyFactory;

        /// <summary>
        /// Character's memory. Contains his knowledge and opinions about stuff.
        /// </summary>
        //[NonSerialized]
        internal Memory.Memory MyMemory;

        /// <summary>
        /// This is the defining characteristics of the personality.
        /// Why is it private and only accessible by getters?
        /// Remember: You can't change someone. They have to choose to change.
        /// </summary>
        internal CharacterTraits MyTraits;

        public event EventHandler<List<Reaction>> CharacterReacted;

        //100% = I'm awake
        private int _wakingProbability = 100;    //TODO
        #endregion

        #region Constructor and Character Initialization, Sleep and Hibernate

        /// <summary>
        /// Character constructor.
        /// This is nature. It can be changed by nurture.
        /// </summary>
        /// <param name="allAboutMe">Hey, who am I, really? What's my name, my gender, stuff like that. Let me know.</param>
        /// <param name="archetype">Is my character inspired by an archetype, 
        /// or am I a product of pure randomness (null/empty value)</param>
        /// <param name="commonKnowledgeContent">The character's basic knowledge</param>
        public Character(Person allAboutMe, Archetype archetype, List<MemoryItem> commonKnowledgeContent = null)
        {
            UniqueId = Guid.NewGuid();
            if (allAboutMe == null)
            {
                throw new ArgumentNullException(ErrorMessages.IHaveNoPersonality);
            }

            if (string.IsNullOrEmpty(allAboutMe.Name))
            {
                throw new ArgumentNullException(ErrorMessages.IHaveNoName);
            }

            MyTraits = new CharacterTraits(allAboutMe.Name, allAboutMe.Sex, allAboutMe.Orientation, allAboutMe.Gender);
            InitializeTraits(archetype);

            MyTraits.ImTired += ImTiredNow;

            MyMemory = new Memory.Memory(allAboutMe);
            AddContentToLongTermMemory(commonKnowledgeContent);
        }

        /// <summary>
        /// When I'm too tired, I have to  go to sleep
        /// </summary>
        /// <param name="sender">who sent the event (it's the character traits</param>
        /// <param name="energy">my current level of energy</param>
        private void ImTiredNow(object sender, int energy)
        {
            //if I'm exhausted I have no choice; I'll go to bed wherever I am.
            if (energy <= PointOfExhaustion)
            {
                GoToSleep(new LearningController());      //TODO FIX!
                CharacterReacted?.Invoke(this, new List<Reaction>
                                    {
                                        new Reaction
                                        {
                                            InitialEvent = null,
                                            Source = MyName,
                                            Target = "General",
                                            EventType = EventType.Unknown,
                                            Intent = Intent.Neutral,
                                            Message = string.Format(CharacterMessages.Tired, MyMemory.Me.Name)
                                        }
                                    });
                return;
            }

            CharacterReacted?.Invoke(this, new List<Reaction>
            {
                new Reaction
                {
                    InitialEvent = null,
                    Source = MyName,
                    Target = "General",
                    EventType = EventType.Unknown,
                    Intent = Intent.Neutral,
                    Message = string.Format(CharacterMessages.Exhausted, MyMemory.Me.Name)
                }
            });
        }

        /// <summary>
        /// Initializes attributes according to the archetype
        /// </summary>
        /// <param name="archetype">The character archetype to generate</param>
        private void InitializeTraits(Archetype archetype)
        {
            switch (archetype)
            {
                case Archetype.None:
                    new RandomInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheInnocent:
                    new TheInnocentInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheOrphan:
                    new TheOrphanInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheWarrior:
                    new TheWarriorInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheCaregiver:
                    new TheCaregiverInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheSeeker:
                    new TheSeekerInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheLover:
                    new TheLoverInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheDestroyer:
                    new TheDestroyerInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheCreator:
                    new TheCreatorInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheRuler:
                    new TheRulerInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheMagician:
                    new TheMagicianInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheSage:
                    new TheSageInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
                case Archetype.TheJester:
                    new TheJesterInitializationMethod().Initialize(ref MyTraits, new QualityRuleEvaluator(),
                        new EmotionRuleEvaluator());
                    break;
            }
        }

        internal void WakeUp(IMemoryFileController fileController, bool useMemoryBackupAsFailsafe = false)
        {
            var memory = fileController.ReadFromFile(UniqueId, useMemoryBackupAsFailsafe);

            if (memory == null)
                throw new RnpcMemoryException("Memory file could not be loaded for character with guid" + UniqueId);

            MyMemory = memory;
        }

        internal void Hibernate(IMemoryFileController fileController, bool saveMemoryBackup = false)
        {
            fileController.WriteToFile(UniqueId, MyMemory, saveMemoryBackup);

            if (!fileController.FileExists(UniqueId))
                throw new RnpcMemoryException(
                    $"An error has prevented memory file for character {MyName} with guid {UniqueId} from being written.");
        }

        /// <summary>
        /// When the character goes to sleep, the learning algorithm kicks in
        /// </summary>
        internal void GoToSleep(ILearningController controller)
        {
            _wakingProbability = (MyTraits.Awareness + (100 - MyTraits.Confidence)) / 2;
            //TODO start thread

            controller.LearnFromMyExperiences(this, FileController, DecisionTreeBuilder);

            _wakingProbability = 100;
        }

        #endregion

        #region Knowledge

        public void AddContentToLongTermMemory(List<MemoryItem> commonKnowledgeContent)
        {
            if (commonKnowledgeContent == null)
                return;

            if (KnowledgeRandomizer == null)
                KnowledgeRandomizer = new StandardKnowledgeRandomization(MyTraits);

            MyMemory.AddLongTermMemoryContent(KnowledgeRandomizer.BuildRandomizedKnowledgeBase(commonKnowledgeContent));
        }

        public void AddContentToLongTermMemory(MemoryItem commonKnowledgeContent)
        {
            if (commonKnowledgeContent == null)
                return;

            if (KnowledgeRandomizer == null)
                KnowledgeRandomizer = new StandardKnowledgeRandomization(MyTraits);

            MyMemory.AddLongTermMemoryContent(KnowledgeRandomizer.RandomizeKnowledgeItem(commonKnowledgeContent));
        }

        /// <summary>
        /// To be called once lifetime training is done.
        /// Used to make sure it is only done once. 
        /// </summary>
        public void TrainingDone()
        {
            HasBeenTrained = true;
        }

        #endregion

        #region Desires

        // ReSharper disable once UnusedMember.Local
        private static List<Reaction> ActUponMyDesires()
        {
            //TODO
            throw new NotImplementedException("Set desires!");
        }

        #endregion

        #region Event Management

        /// <summary>
        /// Socially interact with this character
        /// </summary>
        /// <param name="interaction">The action that the character is reacting to</param>
        /// <returns>The character's reaction</returns>
        public List<Reaction> InteractWithMe(RNPC.Core.Action.Action interaction)
        {
            if(interaction == null)
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Source = MyName,
                        Target = null,
                        EventType = EventType.Interaction,
                        Intent = Intent.Neutral,
                        Message = ErrorMessages.WhatIsGoingOn
                    }
                };

            //I'm asleep. You can try to wake me up, but it might be hard.
            if (_wakingProbability < 100)
            {
                //You didn't wake me up. I'm still sleeping.
                if(_wakingProbability <= RandomValueGenerator.GeneratePercentileIntegerValue())
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = (MyTraits.Energy <= 10) ? string.Format(CharacterMessages.Snoring, MyMemory.Me.Name) :  
                                                                string.Format(CharacterMessages.Sleeping, MyMemory.Me.Name)
                        }
                    };

                //I woke up. But I'm confused!
                //TODO: interupt learning
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Tone = Tone.Confused,
                        InitialEvent = interaction,
                        Source = MyName,
                        Target = null,
                        EventType = EventType.Interaction,
                        Intent = Intent.Neutral,
                        Message = CharacterMessages.JustWokeUp
                    }
                };
            }

            switch (interaction.EventType)
            {
                //TODO : Log
                case EventType.Environmental:
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = ErrorMessages.EnvironmentEventPassedInWrongWay
                        }
                    };
                //TODO : Log
                case EventType.Temperature:
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = ErrorMessages.TemperatureEventPassedInWrongWay
                        }
                    };
                //TODO : Log
                case EventType.Weather:
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = ErrorMessages.EnvironmentEventPassedInWrongWay
                        }
                    };
                //TODO : Log
                case EventType.Time:
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = ErrorMessages.TimeEventPassedInWrongWay
                        }
                    };
                case EventType.Interaction:
                    return HandleSocialInteraction(interaction);
                case EventType.Biological:
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = ErrorMessages.PhysicalEventPassedInWrongWay
                        }
                    };
                case EventType.Unknown:
                    return new List<Reaction>
                    {
                        new Reaction
                        {
                            InitialEvent = interaction,
                            Source = MyName,
                            Target = null,
                            EventType = EventType.Interaction,
                            Intent = Intent.Neutral,
                            Message = ErrorMessages.WhatIsGoingOn
                        }
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Handles all social interactions with the character
        /// </summary>
        /// <param name="action">The action that the character is reacting to</param>
        /// <returns>The character's reaction</returns>
        private List<Reaction> HandleSocialInteraction(RNPC.Core.Action.Action action)
        {
            try
            {
                if (FileController == null || DecisionTreeBuilder == null)
                    throw new RnpcParameterException("Objects FileController or DecisionTreeBuilder has not been properly initialized.");



                var decisionTreeRootNode = DecisionTreeBuilder.BuildTreeFromDocument(FileController, action, MyName);

                if (decisionTreeRootNode == null)
                    throw new NullNodeException("Root node has not been initialized. Check BuildTreeFromDocument method.");

                //var contextInfo = ContextAnalyzer.EvaluateEventContext(action);

                //var strategy = ContextStrategyFactory.GetContextStrategy(contextInfo);

                //var reaction = strategy.Evaluate(this, action, decisionTreeRootNode);

                var reaction = decisionTreeRootNode.Evaluate(MyTraits, MyMemory, action);

                MyMemory.AddNodeTestResults(((AbstractDecisionNode)decisionTreeRootNode).GetNodeTestsData());

                reaction[0].ReactionScore = ((AbstractDecisionNode)decisionTreeRootNode).GetNodeTestsData().Sum(info => info.ProfileScore);

                MyMemory.AddRecentReactions(reaction);

                return reaction;

                //return strategy.Evaluate(this, action, decisionTreeRootNode);
            }
            //TODO: logging
            catch (XmlException exception)
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Confused,
                        InitialEvent = action,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "XmlException",
                        ErrorMessages = exception.Message,
                        Message = ErrorMessages.IDontKnow,
                        Source = MyName
                    }
                };
            }
            catch (NullNodeException exception)
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Confused,
                        InitialEvent = action,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "NullNodeException",
                        ErrorMessages = exception.Message,
                        Message = ErrorMessages.ICantDecide,
                        Source = MyName
                    }
                };
            }
            catch (RnpcParameterException exception)
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Pensive,
                        InitialEvent = action,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "RnpcParameterException",
                        ErrorMessages = exception.Message,
                        Message = ErrorMessages.FeelsLike,
                        Source = MyName
                    }
                };
            }
            catch (NodeInitializationException exception
            ) //The decision tree could not be initialized properly. Check the xml files.
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Confused,
                        InitialEvent = action,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "NodeInitializationException",
                        ErrorMessages = exception.Message,
                        Message = ErrorMessages.MyThoughtsAreJumbled,
                        Source = MyName
                    }
                };
            }
            catch (Exception e) //A generic exception was raised while initializing the decision tree 
            {
                return new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Confused,
                        InitialEvent = action,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "Exception",
                        ErrorMessages = e.Message,
                        Message = ErrorMessages.IHaveAHeadache,
                        Source = MyName
                    }
                };
            }
        }

        /// <summary>
        /// Manages Environmental events
        /// </summary>
        /// <param name="e">That thing that happened that someone noticed. You know.</param>
        /// <param name="caller">Only Cronos should call this method. We have our eye on you!</param>
        internal void NotifyEnvironmentalEvent(PerceivedEvent e, object caller)
        {
            if (caller == null || caller.ToString() != "RNPC.API.Cronos")
            {
                var reaction = new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Confused,
                        InitialEvent = e,
                        ReactionScore = 0,
                        Target = null,
                        EventName = string.Empty,
                        Message = ErrorMessages.MethodCallerInvalid,
                        Source = MyName
                    }
                };

                CharacterReacted?.Invoke(this, reaction);
                return;
            }

            if (e.EventType == EventType.Environmental)
            {
                var reaction =
                new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Indifferent,
                        InitialEvent = e,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "Proxy event",
                        Message = ErrorMessages.IDontKnow,
                        Source = MyName
                    }
                };
                //TODO
                CharacterReacted?.Invoke(this, reaction);
                return;
            }

            if (e.EventType == EventType.Temperature)
            {
                var reaction = new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Indifferent,
                        InitialEvent = e,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "Proxy event",
                        Message = ErrorMessages.IDontKnow,
                        Source = MyName
                    }
                };
                //TODO
                CharacterReacted?.Invoke(this, reaction);
                return;
            }

            if (e.EventType == EventType.Weather)
            {
                var reaction = new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Indifferent,
                        InitialEvent = e,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "Proxy event",
                        Message = ErrorMessages.IDontKnow,
                        Source = MyName
                    }
                };
                //TODO
                CharacterReacted?.Invoke(this, reaction);
                return;
            }

            if (e.EventType == EventType.Time)
            {
                //TODO validate
                if (!IsStrictlyReactive)
                {
                    CharacterReacted?.Invoke(this, ActUponMyDesires());
                }

                var reaction = new List<Reaction>
                {
                    new Reaction
                    {
                        Intent = Intent.Neutral,
                        ActionType = ActionType.Verbal,
                        EventType = EventType.Interaction,
                        Tone = Tone.Indifferent,
                        InitialEvent = e,
                        ReactionScore = 0,
                        Target = null,
                        EventName = "Proxy event",
                        Message = ErrorMessages.IDontKnow,
                        Source = MyName
                    }
                };
                //TODO
                CharacterReacted?.Invoke(this, reaction);
            }
        }
        #endregion
    }
}