using RNPC.Core.Learning.Interfaces;

namespace RNPC.Core.Learning.Memories
{
    internal class MainMemoryLearningStrategy : ILearningStrategy
    {
        public bool AnalyzeAndLearn(Character learningCharacter)
        {
            learningCharacter.AddContentToLongTermMemory(learningCharacter.MyMemory.GetShortTerMemoryItems());
            return true;
        }
    }
}