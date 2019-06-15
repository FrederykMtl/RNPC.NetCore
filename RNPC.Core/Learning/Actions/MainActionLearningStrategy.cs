using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.Resources;
using RNPC.Core.TraitGeneration;

namespace RNPC.Core.Learning.Actions
{
    internal class MainActionLearningStrategy : ILearningStrategy
    {
        public bool AnalyzeAndLearn(Character learningCharacter)
        {
            var recentActions = learningCharacter.MyMemory.GetMyRecentActions();

            foreach (Action.Action recentAction in recentActions)
            {
                if (recentAction.AssociatedKarma <= LearningParameters.UnforgivableActionThreshold)
                {
                    learningCharacter.MyMemory.AddActionToLongTermMemory(recentAction);
                    learningCharacter.MyTraits.RaiseMyShame(10);
                }

                if (recentAction.AssociatedKarma >= LearningParameters.UnforgettableActionThreshold)
                {
                    learningCharacter.MyMemory.AddActionToLongTermMemory(recentAction);
                    learningCharacter.MyTraits.RaiseMyPride(10);
                }

                if (learningCharacter.MyTraits.Memory > RandomValueGenerator.GeneratePercentileIntegerValue())
                    learningCharacter.MyMemory.AddActionToLongTermMemory(recentAction);
            }

            learningCharacter.MyMemory.ResetRecentActions();

            return true;
        }
    }
}