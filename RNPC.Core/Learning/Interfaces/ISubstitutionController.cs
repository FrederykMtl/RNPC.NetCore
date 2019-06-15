using RNPC.Core.Interfaces;

namespace RNPC.Core.Learning.Interfaces
{
    public interface ISubstitutionController
    {
        bool SubstituteNode(ITreeBuilder builder, string characterName, Action.Action action, string decisionTreeToChange);
        bool PruneDecisionTree(ITreeBuilder builder, string characterName, Action.Action action, string decisionTreeToChange);
    }
}