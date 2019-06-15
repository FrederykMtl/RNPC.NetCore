using System;
using RNPC.Core.Enums;

namespace RNPC.Core
{
    [Serializable]
    public class EmotionalStates
    {
        [Emotional]
        public int Anger { get; internal set; }
        [Emotional]
        public int Fear { get; internal set; }
        [Emotional]
        public int Happiness { get; internal set; }
        [Emotional]
        public int Sadness { get; internal set; }
        [Emotional]
        public int Disgust { get; internal set; }
        [Emotional]
        public int Curiosity { get; internal set; }
        [Emotional]
        public int Surprise { get; internal set; }
        [Emotional]
        public int Jealousy { get; internal set; }
        [Emotional]
        public int Shame { get; internal set; }
        [Emotional]
        public int Disappointment { get; internal set; }
        [Emotional]
        public int Pride { get; internal set; }
    }
}
