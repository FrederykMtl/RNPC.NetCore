using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using RNPC.API.Training;
using RNPC.Core;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Core.GameTime;
using RNPC.Core.Interfaces;
using RNPC.Core.KnowledgeBuildingStrategies;
using RNPC.Core.Memory;
using RNPC.Core.Resources;
using RNPC.FileManager;

[assembly: InternalsVisibleTo("RNPC.Tests.Functional")]
[assembly: InternalsVisibleTo("RNPC.Tests.Unit")]

namespace RNPC.API
{
    /// <summary>
    /// Singleton class that acts as global controller for all characters
    /// </summary>
    public class Cronos
    {
        //instance of the class
        private static Cronos _godOfTime;
        //The Sum of All Knowledge
        internal Omniscience MyOmniscience;
        private GameTime _gameTime;

        //#region Public properties
        //TODO
        /// <summary>
        /// Is this implementation part of the test phase of  your project?
        /// This allows to save all the chracters' progress to centralized files, allowing them
        /// to evolve from interacting with all your testers.
        /// </summary>
        public bool IsInTestingPhase = false;

        public event EventHandler<Character> OneMonthTrained;
        public event EventHandler<Character> TrainingComplete;

        //The controller used to load/save all knowledge
        public IOmniscienceFileController FileController;
        //#endregion

        /// <summary>
        /// God of time, there can only be one of.
        /// </summary>
        private Cronos()
        {
            MyOmniscience = new Omniscience();
            //set all the config paths
            ConfigurationDirectory.Instance.KnowledgeFilesDirectory = ConfigurationManager.AppSettings["KnowledgeFilesDirectory"];
            ConfigurationDirectory.Instance.CentralDecisionTreeRepository = ConfigurationManager.AppSettings["CentralDecisionTreeRepository"];
            ConfigurationDirectory.Instance.CharacterFilesDirectory = ConfigurationManager.AppSettings["CharacterFilesDirectory"];
            ConfigurationDirectory.Instance.LogFilesDirectory = ConfigurationManager.AppSettings["LogFilesDirectory"];
            ConfigurationDirectory.Instance.NodeSubstitutionsFile = ConfigurationManager.AppSettings["NodeSubstitutionsFile"];
            ConfigurationDirectory.Instance.SubTreeRepository = "..\\..\\..\\..\\RNPC\\Subtrees\\";

            ConfigurationDirectory.Instance.NodeSubstitutionsFile = "..\\..\\..\\..\\DTO\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            ConfigurationDirectory.Instance.CentralDecisionTreeRepository = "..\\..\\..\\..\\RNPC\\XMLTreeFiles\\";
            ConfigurationDirectory.Instance.CharacterFilesDirectory = "C:\\Sysdev\\RNPC\\logs\\Characters\\";
            ConfigurationDirectory.Instance.KnowledgeFilesDirectory = "C:\\Sysdev\\RNPC\\Knowledge\\";
            ConfigurationDirectory.Instance.LogFilesDirectory = "C:\\Sysdev\\RNPC\\logs\\";
        }

        #region Public methods
        public static Cronos Instance => _godOfTime ?? (_godOfTime = new Cronos());

        public GameTime GetCurrentTime()
        {
            return _gameTime ?? (_gameTime = new StandardDateTime(DateTime.Now));
        }

        public void SetGameTime(GameTime time)
        {
            if (time == null)
                _gameTime = new StandardDateTime(DateTime.Now);

            _gameTime = time;
        }

        #region File saving and loading
        /// <summary>
        /// Activates the function to save a backup of memory / omniscience file
        /// </summary>
        public void ActivateMemoryBackups()
        {
            MyOmniscience.BackupMemoryFiles = true;
        }

        public void DeactivateMemoryBackups()
        {
            MyOmniscience.BackupMemoryFiles = false;
        }

        public void Hibernate()
        {
            if (FileController == null)
                FileController = new OmniscienceFileController();

            FileController.WriteToFile(MyOmniscience, MyOmniscience.BackupMemoryFiles);

            if (!FileController.FileExists())
                throw new RnpcOmniscienceException($"An error has prevented the Omniscience file from being written.");

            foreach (var character in MyOmniscience.MyFollowers)
            {
                character.Hibernate(new MemoryFileController(ConfigurationDirectory.Instance.KnowledgeFilesDirectory), MyOmniscience.BackupMemoryFiles);
            }
        }

        public void WakeUp()
        {
            if(FileController == null)
                FileController = new OmniscienceFileController();

            var allKnowledge = FileController.ReadFromFile(MyOmniscience.BackupMemoryFiles);

            if (allKnowledge == null)
                throw new RnpcOmniscienceException("The Omniscience file for Cronos could not be properly loaded.");

            MyOmniscience = allKnowledge;

            foreach (var character in MyOmniscience.MyFollowers)
            {
                character.WakeUp(new MemoryFileController(ConfigurationDirectory.Instance.KnowledgeFilesDirectory), MyOmniscience.BackupMemoryFiles);
            }
        }

        #endregion

        #region Characters

        /// <summary>
        /// Only followers get to hear the divine word!
        /// They're also the only ones who get to sleep...
        /// </summary>
        /// <param name="newCharacter">Character to add to the list of characters to be notified of events,
        /// including passage of time, natural events, nearby events, and also the directive to go to sleep.</param>
        public void AddFollower(Character newCharacter)
        {
            MyOmniscience.AddFollower(newCharacter);
        }

        public void NotifyAllCharactersInRegion(PerceivedEvent eventInfo, Coordinates centerPoint, decimal radius)
        {
            foreach (var character in MyOmniscience.MyFollowers)
            {
                if(character.MyMemory == null)
                    continue;

                //TODO : FIX
                if (character.MyMemory.MyCurrentLocation?.Coordinates == null)
                {
                    character.NotifyEnvironmentalEvent(eventInfo, this);
                }
                else if (character.MyMemory.MyCurrentLocation.Coordinates.Equals(centerPoint))
                {
                    character.NotifyEnvironmentalEvent(eventInfo, this);
                }
            }
        }

        public Character CreateCharacter(Person characterInformation, Archetype characterArchetype, List<MemoryItem> knowledgeBase = null)
        {
            var fileController = new DecisionTreeFileController();

            var newCharacter = new Character(characterInformation, characterArchetype)
                {
                    DecisionTreeBuilder = new DecisionTreeBuilder(),
                    FileController = fileController
            };

            newCharacter.KnowledgeRandomizer = new StandardKnowledgeRandomization(newCharacter.MyTraits);

            newCharacter.AddContentToLongTermMemory(knowledgeBase);

            fileController.InitializeCharacterDecisionTrees(characterInformation.Name, true);

            AddFollower(newCharacter);

            return newCharacter;
        }

        public void TrainCharacterForOneYear(Character characterToTrain)
        {
            var trainer = new PersonalTrainer(new ItemLinkFactory());

            trainer.OneMonthTrained += TrainerOnOneMonthTrained;

            trainer.TrainForAYear(characterToTrain);

            TrainingComplete?.Invoke(this, characterToTrain);
        }

        private void TrainerOnOneMonthTrained(object sender, Character e)
        {
            OneMonthTrained?.Invoke(sender, e);
        }

        public void TrainCharacterForFiveYear(Character characterToTrain)
        {
            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());

            trainer.OneMonthTrained += TrainerOnOneMonthTrained;

            trainer.TrainForXNumberOfYears(characterToTrain, 5);

            TrainingComplete?.Invoke(this, characterToTrain);
        }

        public void TrainCharacterForALifetime(Character characterToTrain)
        {
            PersonalTrainer trainer = new PersonalTrainer(new ItemLinkFactory());

            trainer.OneMonthTrained += TrainerOnOneMonthTrained;

            trainer.TrainForALifetime(characterToTrain, new StandardDateTime(DateTime.Now)); //TODO: Change

            TrainingComplete?.Invoke(this, characterToTrain);
        }

        #endregion

        #region Knowledge
        //public List<MemoryItem> GetLocalizedCommonKnowledge(string areaName)
        //{
        //    return MyOmniscience.LocalisedKnowledge[areaName];
        //}

        //public void AddLocalizedCommonKnowledge(string areaName, List<MemoryItem> knowledge)
        //{
        //    MyOmniscience.LocalisedKnowledge.Add(areaName, knowledge);
        //}

        //public void AddReferenceData(MemoryItem data)
        //{
        //    MyOmniscience.ReferenceData.Add(Guid.NewGuid(), data);
        //}

        //public MemoryItem FindReferenceDataByGuid(Guid referenceGuid)
        //{
        //    return MyOmniscience.ReferenceData[referenceGuid];
        //}

        public void AddReferenceDataList(List<MemoryItem> dataList)
        {
            foreach (MemoryItem item in dataList)
            {
                MyOmniscience.ReferenceData.Add(Guid.NewGuid(), item);
            }
        }

        internal Omniscience GetAllKnowledge()
        {
            return MyOmniscience;
        }
        #endregion

        #endregion
    }
}