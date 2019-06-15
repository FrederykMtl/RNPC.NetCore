using RNPC.Core.Interfaces;
using RNPC.Core.Learning.Interfaces;
using RNPC.Core.Learning.LearningTemplateMethod;

namespace RNPC.Core.Learning
{
    public class LearningController : ILearningController
    {
        /// <inheritdoc />
        public void LearnFromMyExperiences(Character learningCharacter, IXmlFileController fileController, ITreeBuilder builder)
        {
            //TODO: Test for different psychological illnesses when it is implemented.
            MainLearningMethod learningMethod = new MainLearningMethod(fileController, builder);
            learningMethod.LearnFromMyExperiences(learningCharacter);
        }
    }
}