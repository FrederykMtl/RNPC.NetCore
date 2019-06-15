using RNPC.Core.Interfaces;

// ReSharper disable once CheckNamespace
namespace RNPC.Core.Learning.Interfaces
{
    internal interface ILearningController
    {
        /// <summary>
        /// Main learning algorithm. For each class it will apply the appropriate strategy to analyse
        /// recent experiences and evolve the character accordingly.
        /// </summary>
        /// <param name="learningCharacter">The character that is learning.</param>
        /// <param name="fileController">The controller used to read XML files used in the learning process.</param>
        /// <param name="builder">Decision tree builder</param>
        void LearnFromMyExperiences(Character learningCharacter, IXmlFileController fileController, ITreeBuilder builder);
    }
}
