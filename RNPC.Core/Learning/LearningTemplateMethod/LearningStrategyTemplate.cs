using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.Resources;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Learning.LearningTemplateMethod
{
    //TODO Manage exceptions and problems
    internal abstract class LearningStrategyTemplate
    {
        protected ILearningStrategy ActionLearningStrategy;
        protected ILearningStrategy MemoryLearningStrategy;
        protected ILearningStrategy EmotionLearningStrategy;
        protected ILearningStrategy OpinionLearningStrategy;

        protected ILearningStrategy QualityLearningStrategy;
        protected ILearningStrategy PersonalValueLearningStrategy;
        protected ILearningStrategy DecisionTreeLearningStrategy;
        protected ILearningStrategy DesireLearningStrategy;

        public void LearnFromMyExperiences(Character learningCharacter)
        {
            if (!LearningThresholdReached(learningCharacter.MyMemory))
                return;

            //TODO : Thead this
            AdjustEmotionalStates(learningCharacter);

            FormOpinions(learningCharacter);

            AdjustDesires(learningCharacter);

            AdjustQualityTraits(learningCharacter);

            AdjustPersonalValues(learningCharacter);

            TransfertRecentActionsToLongTerm(learningCharacter);

            //Decision trees analyze actions on the long term. Recent must be 
            //transfered to long term beforehand to make it easier.
            EvolveDecisionTrees(learningCharacter);
            //Speech patterns

            TransfertMemoriesToLongTerm(learningCharacter);

            //Resetting Values
            ResetMemoryItems(learningCharacter.MyMemory);
            ResetNodeResults(learningCharacter.MyMemory);
            learningCharacter.MyTraits.ResetEmotions();
            //actions?
        }

        //public void LearnFromMyExperiences(Character learningCharacter)
        //{
        //    if (!LearningThresholdReached(learningCharacter.MyMemory))
        //        return;

        //    long[] times = new long[9];
        //    var timer = new Stopwatch();

        //    timer.Start();
        //    //TODO : Thead this
        //    AdjustEmotionalStates(learningCharacter);
        //    timer.Stop();
        //    times[0] = timer.ElapsedMilliseconds;
        //    timer.Start();

        //    //FormOpinions(learningCharacter);
        //    //timer.Stop();
        //    //times[1] = timer.ElapsedMilliseconds;
        //    //timer.Start();

        //    //AdjustDesires(learningCharacter);
        //    //timer.Stop();
        //    //times[2] = timer.ElapsedMilliseconds;
        //    //timer.Start();

        //    AdjustQualityTraits(learningCharacter);
        //    timer.Stop();
        //    times[3] = timer.ElapsedMilliseconds;
        //    timer.Start();

        //    AdjustPersonalValues(learningCharacter);
        //    timer.Stop();
        //    times[4] = timer.ElapsedMilliseconds;
        //    timer.Start();

        //    TransfertRecentActionsToLongTerm(learningCharacter);
        //    timer.Stop();
        //    times[5] = timer.ElapsedMilliseconds;
        //    timer.Start();

        //    //Decision trees analyze actions on the long term. Recent must be 
        //    //transfered to long term beforehand to make it easier.
        //    EvolveDecisionTrees(learningCharacter);
        //    //Speech patterns
        //    timer.Stop();
        //    times[6] = timer.ElapsedMilliseconds;
        //    timer.Start();

        //    TransfertMemoriesToLongTerm(learningCharacter);
        //    timer.Stop();
        //    times[7] = timer.ElapsedMilliseconds;
        //    timer.Start();

        //    //Resetting Values
        //    ResetMemoryItems(learningCharacter.MyMemory);
        //    ResetNodeResults(learningCharacter.MyMemory);
        //    learningCharacter.MyTraits.ResetEmotions();

        //    timer.Stop();
        //    times[8] = timer.ElapsedMilliseconds;

        //    File.AppendAllText("C:\\logs\\log.txt", string.Join("\t", times) + Environment.NewLine);
        //    //actions?
        //}

        protected virtual bool LearningThresholdReached(Memory.Memory memory)
        {
            return memory.GetNodeTestResultsCount() >=  LearningParameters.MinimumLearningThreshold;
        }

        //TODO Control results
        protected void AdjustDesires(Character learningCharacter)
        {
            if(!DesireLearningStrategy.AnalyzeAndLearn(learningCharacter))
                return; //TODO
        }

        protected void EvolveDecisionTrees(Character learningCharacter)
        {
            DecisionTreeLearningStrategy.AnalyzeAndLearn(learningCharacter);
        }

        protected void AdjustPersonalValues(Character learningCharacter)
        {
            PersonalValueLearningStrategy.AnalyzeAndLearn(learningCharacter);
        }

        protected void AdjustQualityTraits(Character learningCharacter)
        {
            QualityLearningStrategy.AnalyzeAndLearn(learningCharacter);
        }

        protected void FormOpinions(Character learningCharacter)
        {
            OpinionLearningStrategy.AnalyzeAndLearn(learningCharacter);
        }

        protected void AdjustEmotionalStates(Character learningCharacter)
        {
            EmotionLearningStrategy.AnalyzeAndLearn(learningCharacter);
        }

        protected void TransfertRecentActionsToLongTerm(Character learningCharacter)
        {
            ActionLearningStrategy.AnalyzeAndLearn(learningCharacter);
            learningCharacter.MyMemory.ArchiveMyMemoryOfPastActions();
        }

        protected void TransfertMemoriesToLongTerm(Character learningCharacter)
        {
            MemoryLearningStrategy.AnalyzeAndLearn(learningCharacter);
        }

        protected void ResetMemoryItems(Memory.Memory memory)
        {
            memory.ResetShortTermMemory();
        }

        protected void ResetNodeResults(Memory.Memory memory)
        {
            memory.ResetNodeTestResults();
        }
    }
}
