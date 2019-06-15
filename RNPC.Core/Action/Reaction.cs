using System;

namespace RNPC.Core.Action
{
    /// <inheritdoc />
    /// <summary>
    /// A reaction to an event of any type.
    /// </summary>
    [Serializable]
    public class Reaction : Action
    {
        //Used to give an overall score to the reaction, 
        //allowing future psychological profiling
        public double ReactionScore;
        //Event that is reacted to.
        public PerceivedEvent InitialEvent;
        //Errors that may have occurred
        public string ErrorMessages;
        //time in second before the next reaction should be processed
        public int IntervalToNextReaction = 0;
    }
}