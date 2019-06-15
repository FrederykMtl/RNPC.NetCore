using System;
using System.Collections.Generic;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Omniscience
    {
        #region Properties

        public List<Character> MyFollowers { get; set; }

        /// <summary>
        /// This is a dictionary that contains all the information that is considered
        /// as a proven and provable truth within the game universe.
        /// It should be used for common knowledge items only.
        /// Knwolege held by characters is considered as part of their belief system,
        /// and as such is not considered a reference; knowlegde will be copied from this
        /// repository and incorrect information will bee introduced to reprensent that fact.
        /// Remember, some people believe the Earth is flat!
        /// </summary>
        public Dictionary<Guid, MemoryItem> ReferenceData { get; }

        public Dictionary<string, List<MemoryItem>> LocalisedKnowledge { get; }

        #endregion

        #region Public properties
        /// <summary>
        /// Should we create a backup before we save new information about the characters?
        /// </summary>
        public bool BackupMemoryFiles = false;
        #endregion

        #region Constructors

        public Omniscience()
        {
            MyFollowers = new List<Character>();
            ReferenceData = new Dictionary<Guid, MemoryItem>();
            LocalisedKnowledge = new Dictionary<string, List<MemoryItem>>();
        }

        public Omniscience(Dictionary<Guid, MemoryItem> referenceData)
        {
            MyFollowers = new List<Character>();
            ReferenceData = referenceData;
            LocalisedKnowledge = new Dictionary<string, List<MemoryItem>>();
        }

        public Omniscience(Dictionary<Guid, MemoryItem> referenceData, Dictionary<string, List<MemoryItem>> localisedKnowledge)
        {
            MyFollowers = new List<Character>();
            ReferenceData = referenceData;
            LocalisedKnowledge = localisedKnowledge;
        }

        #endregion

        public void AddFollower(Character newFollower)
        {
            MyFollowers.Add(newFollower);
        }
    }
}