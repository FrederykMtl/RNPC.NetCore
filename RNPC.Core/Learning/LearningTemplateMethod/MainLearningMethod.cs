using RNPC.Core.Interfaces;
using RNPC.Core.Learning.Actions;
using RNPC.Core.Learning.DecisionTrees;
using RNPC.Core.Learning.Desires;
using RNPC.Core.Learning.Emotions;
using RNPC.Core.Learning.Memories;
using RNPC.Core.Learning.Opinions;
using RNPC.Core.Learning.Qualities;
using RNPC.Core.Learning.Substitutions;
using RNPC.Core.Learning.Values;

namespace RNPC.Core.Learning.LearningTemplateMethod
{
    internal class MainLearningMethod : LearningStrategyTemplate
    {
        internal MainLearningMethod(IXmlFileController fileController, ITreeBuilder builder)
        {
            ActionLearningStrategy = new MainActionLearningStrategy();

            var controller = new SubstitutionController(new SubstitutionDocumentConverter(), fileController, new SubstitutionMapper());
            DecisionTreeLearningStrategy = new MainDecisionTreeLearningStrategy(controller, builder);
            DesireLearningStrategy = new MainDesireLearningStrategy();
            EmotionLearningStrategy = new MainEmotionLearningStrategy();

            MemoryLearningStrategy = new MainMemoryLearningStrategy();
            OpinionLearningStrategy = new MainOpinionLearningStrategy();
            PersonalValueLearningStrategy = new MainPersonalValueLearningStrategy(new PersonalValueAssociations());
            
            QualityLearningStrategy = new MainQualityLearningStrategy();
        }
    }
}