using System;
using System.Collections.Generic;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Learning.Resources;

namespace RNPC.Core.Memory
{
    [Serializable]
    public partial class Memory
    {
        public Person Me { get; }
        public Place MyCurrentLocation { get; set; }

        private const int MaximumActionsRemembered = 30;

        #region Long Term Memory
        /// <summary>
        /// This is long-term, permanent knowledge. What is stored here is determined
        /// randomly based on a character's memory (Except for opinions. We always manage to remember those...)
        /// </summary>
        private readonly List<MemoryItem> _longTermMemory;  //TODO: priorisation system
        //Things I've done that were so good I won't forget
        private readonly List<Action.Action> _myUnforgettableActions;
        //Things I've done that were so bad I won't forget
        private readonly List<Action.Action> _myUnforgivableActions;
        //private readonly Hashtable _myPastActions;
        private readonly List<Action.Action> _myPastActions;

        private readonly List<Opinion> _myOpinions;
        //private string[] _mySpeechPatterns;     //TODO

        #endregion

        #region Short Term Memory
        /// <summary>
        /// What is stored here is all the recent information. This will be used by the learning algorithmn
        /// to determine what aspects of the character evolves. Part of it will make its way to long term
        /// memory, based on the character's memory, then the content of these lists will be reset.
        /// </summary>
        private readonly List<MemoryItem> _shortTermMemory;
        private readonly List<Action.Action> _myRecentActions;
        private readonly List<NodeTestInfo> _myNodeTestResults;

        #endregion

        //This list of desires with their importance value
        private List<KeyValuePair<int, Desire>> _myDesires;
        //This is a score indicating a character's "moral score", an indication
        //of its perception as a good or bad person. It is entirely psychological.
        internal int MyKarmaScore;

        #region Constructors
        public Memory(List<MemoryItem> longTermMemory, Person me):this(me)
        {
            _longTermMemory = longTermMemory ?? new List<MemoryItem>();
        }

        public Memory(Person me)
        {
            Me = me; 
            _myRecentActions = new List<Action.Action>();
            _myPastActions = new List<Action.Action>();
            _myUnforgettableActions = new List<Action.Action>();
            _myUnforgivableActions = new List<Action.Action>();
            _shortTermMemory = new List<MemoryItem>();
            _longTermMemory = new List<MemoryItem>();
            _myOpinions = new List<Opinion>();
            _myNodeTestResults = new List<NodeTestInfo>();
            _myDesires = new List<KeyValuePair<int, Desire>> { new KeyValuePair<int, Desire>(1, Desire.Nothingness) };
            //_mySpeechPatterns = new string[3];
            Persons = new PersonsInterface(this);
            Occupations = new OccupationsInterface(this);
            Places = new PlacesInterface(this);
            Events = new PastEventsInterface(this);
            Organizations = new OrganizationsInterface(this);
        }
        #endregion

        public void AddNodeTestResults(List<NodeTestInfo> nodeResults)
        {
            _myNodeTestResults.AddRange(nodeResults);
        }

        /// <summary>
        /// Get all the recent test results.
        /// </summary>
        /// <returns></returns>
        public List<NodeTestInfo> GetAllCurrentNodeTestInfos()
        {
            return _myNodeTestResults;
        }

        /// <summary>
        /// Remove all the test results once they have been evaluated.
        /// </summary>
        public void ResetNodeTestResults()
        {
            _myNodeTestResults.Clear();
        }

        /// <summary>
        /// Get the number of results. Used by learning algorithm
        /// </summary>
        /// <returns></returns>
        public int GetNodeTestResultsCount()
        {
            return _myNodeTestResults.Count;
        }

        #region Knowledge management

        internal void AddMemory(MemoryItem shortTermMemory)
        {
            _shortTermMemory.Add(shortTermMemory);
        }

        internal void ResetShortTermMemory()
        {
            _shortTermMemory.Clear();
        }

        internal void ResetItemsForTraining()
        {
            _myRecentActions.Clear(); //= new List<Action.Action>();
            _myPastActions.Clear(); //= new List<Action.Action>();
            _myNodeTestResults.Clear(); // = new List<NodeTestInfo>();
        }

        internal List<MemoryItem> GetShortTerMemoryItems()
        {
            return _shortTermMemory;
        }

        internal void AddLongTermMemoryContent(List<MemoryItem> longTermMemories)
        {
            _longTermMemory.AddRange(longTermMemories);
        }

        internal void AddLongTermMemoryContent(MemoryItem longTermMemory)
        {
            _longTermMemory.Add(longTermMemory);
        }

        /// <summary>
        /// Returns all personal information
        /// </summary>
        /// <returns></returns>
        internal Person GetMyInformation()
        {
            return Me;
        }

        /// <summary>
        /// Returns all the character's knowledge
        /// </summary>
        /// <returns></returns>
        internal List<MemoryItem> WhatDoIKnow()
        {
            return _longTermMemory;
        }

        //For testing and evaluation purposes only.
        internal int HowManyThingsDoIknow()
        {
            return _longTermMemory.Count;
        }
        #endregion

        #region Actions management

        /// <summary>
        /// Add all recent reactions to memory
        /// </summary>
        /// <param name="recentReactions">recent reactions</param>
        internal void AddRecentReactions(List<Reaction> recentReactions)
        {
            _myRecentActions.AddRange(recentReactions);
        }

        internal void AddActionToLongTermMemory(Action.Action recentAction)
        {
            //transfers rememberance of recent actions to long term memory
            _myPastActions.Add(recentAction);
        }

        /// <summary>
        /// Do I have something really bad on my conscience?
        /// </summary>
        /// <returns></returns>
        public bool HaveIDoneSomethingUnforgivable()
        {
            return _myUnforgivableActions.Count > 0;
        }

        /// <summary>
        /// Clears recent actions at the end of the day.
        /// </summary>
        public void ResetRecentActions()
        {
            _myRecentActions.Clear();
        }

        /// <summary>
        /// Returns all of the actions done since the last learning session
        /// </summary>
        /// <returns></returns>
        internal List<Action.Action> GetMyRecentActions()
        {
            return _myRecentActions;
        }

        /// <summary>
        /// Returns all remembered past actions
        /// </summary>
        /// <returns></returns>
        internal IList<Action.Action> GetMyPastActions()
        {
            return _myPastActions;
        }

        /// <summary>
        /// Cleans up the memory of past actions, keeping only recent entries
        /// or really significant events 
        /// </summary>
        internal void ArchiveMyMemoryOfPastActions()
        {
            //nothing to be archived, or too little content.
            if (_myPastActions.Count == 0 || _myPastActions.Count <= MaximumActionsRemembered)
                return;

            int numberOfElementsToRemove = _myPastActions.Count - MaximumActionsRemembered;

            for (int i = 0; i < numberOfElementsToRemove; i++)
            {
                var actionToArchive = _myPastActions[i];

                if (actionToArchive.AssociatedKarma <= LearningParameters.UnforgivableActionThreshold)
                {
                    ICanNeverForgiveMyselfForThis(actionToArchive);
                    continue;
                }

                if (actionToArchive.AssociatedKarma >= LearningParameters.UnforgettableActionThreshold)
                {
                    ICanNeverForgetThis(actionToArchive);
                }
            }

            _myPastActions.RemoveRange(0, numberOfElementsToRemove);
        }

        /// <summary>
        /// It was really, really bad
        /// used by archiving process
        /// </summary>
        /// <param name="unforgivableAction">a very bad actionI won't forget</param>
        // ReSharper disable once InconsistentNaming
        internal void ICanNeverForgiveMyselfForThis(Action.Action unforgivableAction)
        {
            _myUnforgivableActions.Add(unforgivableAction);
        }

        /// <summary>
        /// It was really, really bad
        /// used by archiving process
        /// </summary>
        /// <param name="unforgetableAction">a very good actionI won't forget</param>
        // ReSharper disable once InconsistentNaming
        internal void ICanNeverForgetThis(Action.Action unforgetableAction)
        {
            _myUnforgettableActions.Add(unforgetableAction);
        }

        #endregion

        #region Desires

        internal void InitializeMyDesires(List<KeyValuePair<int, Desire>> desires)
        {
            _myDesires = desires;
        }

        /// <summary>
        /// Returns the character's current, fondest wish
        /// </summary>
        /// <returns>
        /// If my desires have not been established, or I am
        /// a Buddhist that attained oneness then this will return Nothingness.
        /// Otherwise, a value from the enum
        /// </returns>
        internal Desire WhatIsMyGreatestWish()
        {
            if (_myDesires == null || _myDesires.Count == 0)
                return Desire.Nothingness;

            //int highestPriority = 1;
            Desire fondestWish = Desire.Freedom;
            
            //TODO Incorrect!
            //for (int i = 0; i < _myDesires.Count; i++)
            //{
            //    if(_myDesires[i].Key)
            //}


            return fondestWish;
        }

        #endregion

        #region Opinions

        public void FormAnOpinionAbout(Opinion opinion)
        {
            _myOpinions.Add(opinion);
        }

        public void ChangeMyOpinionAbout(MemoryItem opinionAbout, OpinionType newOpinion, string influencingEventDescription)
        {
            //TODO
            //var opinion = _myOpinions.FirstOrDefault(x => x.OpinionAbout == opinion);

            //opinion?.ChangeOpinion(newOpinion, influencingEventDescription);
        }

        public OpinionType WhatIsMyOpinionAbout(MemoryItem opinionAbout)
        {
            foreach (var opinion in _myOpinions)
            {
                if (opinion.OpinionAbout.Equals(opinionAbout))
                    return opinion.Type;
            }

            return OpinionType.NoOpinion;
        }

        internal void RemoveOpinion(Opinion opinionToRemove)
        {
            _myOpinions.Remove(opinionToRemove);
        }

        internal void RemoveOpinionsAbout(Person person)
        {
            for (var index = 0; index < _myOpinions.Count; index++)
            {
                var myOpinion = _myOpinions[index];
                if (myOpinion.OpinionAbout.Equals(person))
                    RemoveOpinion(myOpinion);
            }
        }

        #endregion
    }
}