namespace RNPC.Core.Learning.Resources
{
    public static class LearningParameters
    {
        //Global parameters
        public const bool LockStrongAndWeakPoints = true;       //Whether a character can lose or gain strong and weak points
        public const int MinimumLearningThreshold = 8;         //The minimum number of NodeTestInfo needed for learning to happen.
                                                               //Going through learning will result in the the NodeTestInfo list being reset.
        /// <summary>
        /// Decision tree evolution parameters
        /// </summary>
        public const int DecisionDevolutionThreshold = 6;       //How often should a decision tree be used before it's considered overreliance
        public const int DecisionDevolveRate = 10;
        public const int DecisionLearningRate = 20;             //What is the % of the time a decision tree should evolve
        public const int DecisionLearningThreshold = 3;         //How often should a decision tree be used before we can learn
        /// <summary>
        /// Quality traits learning rate parameters
        /// </summary>
        public const int QualityDevolveRate = 30;               //What is the % of the time a quality trait should go down
        public const int QualityLearningRate = 22;              //What is the % of the time a quality trait should go up
        public const int QualityLearningThreshold = 3;          //How often should a quality be used before we learn, either up or down
        /// <summary>
        /// Karma score for an action tonever be forgotten
        /// </summary>
        public const int UnforgettableActionThreshold = 25;     //How good an action is to be unforgettable
        public const int UnforgivableActionThreshold = -25;     //How bad an action is to be unforgettable
        /// <summary>
        /// Personal values learning parameters
        /// </summary>
        public const int MinimumValueThreshold = 5;             //Minimum number of times a value has to be tested
        public const int ValueAcquisitionRate = 13;             //How often a new value will be acquired
        public const int ValueLossThreshold = 7;                //Minimum number of times a value has to be tested
        public const int ValueLossRate = 30;                     //How often a value can be lost
        //NRVA = Negative reinforcement value acquisition
        //It's based on the idea that failing at a situation involving a value may lead someone to question their values leading them
        //to chosing to adopt that value. It doesn't happen often though.
        public const int NRVAThreshold = 3;                     //Minimum number of times a value has to be failed to be subject to NRVA testing        
        public const int NRVARate = 3;                          //Percentage of chances to acquire that value

    }
}