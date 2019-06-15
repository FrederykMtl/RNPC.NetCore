namespace RNPC.API.Training
{
    /// <summary>
    /// This class has been created to store numeric constants
    /// Avoiding to parse for values which would have been the
    /// case in a resource file
    /// </summary>
    public static class TrainingStatistics
    {
        #region Event statistics
        //distribution percentages for random generation
        public const int Biological = 0;     //EventType
        public const int Environmental = 0; //EventType
        public const int Interaction = 100;  //EventType
        public const int Temperature = 0;   //EventType
        public const int Time = 0;          //EventType
        public const int Weather = 0;        //EventType

        public const int NonVerbal = 25;    //ActionType
        public const int Physical = 0;    //ActionType
        public const int Verbal = 100;    //ActionType

        public const int Friendly = 33; //Intent
        public const int Neutral = 67; //Intent
        public const int Hostile = 100; //Intent

        public const int MinimumEventsPerDay = 5;
        public const int MaximumEventsPerDay = 5;
        public const int StartingAgeOfTraining = 5;
        public const int DaysPerYear = 365;

        #endregion
    }
}