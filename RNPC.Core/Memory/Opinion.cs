using System;
using System.Collections.Generic;
using RNPC.Core.Enums;

namespace RNPC.Core.Memory
{
    [Serializable]
    public class Opinion
    {
        #region Constructors
        public Opinion()
        {
        }

        public Opinion(MemoryItem about)
        {
            OpinionAbout = about;
        }

        public Opinion(MemoryItem about, OpinionType opinion, string influencingEventDescription)
        {
            OpinionAbout = about;
            Type = opinion;
            InfluencingEvents = new List<string> {influencingEventDescription};
        }

        #endregion

        public void ChangeOpinion(OpinionType changeTo, string influencingEventDescription)
        {
            //You can't change your opinion to not having one; you can become neutral
            //but once you have formed an opinion you cannot goback to not having one.
            //You just choose to be neutral.
            Type = changeTo == OpinionType.NoOpinion ? OpinionType.Neutral : changeTo;

            if(InfluencingEvents == null)
                InfluencingEvents = new List<string>();

            InfluencingEvents.Add(influencingEventDescription);
        }

        public bool DoIHaveAGoodOpinionAboutThis()
        {
            return Type == OpinionType.Like || Type == OpinionType.Love;
        }

        public bool DoIHaveABadOpinionAboutThis()
        {
            return Type == OpinionType.Contempt || Type == OpinionType.Hate;
        }

        public MemoryItem OpinionAbout { get; }
        public List<string> InfluencingEvents { get; private set; }
        public OpinionType Type { get; private set; }
    }
}