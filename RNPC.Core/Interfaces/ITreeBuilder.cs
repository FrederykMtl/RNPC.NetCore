namespace RNPC.Core.Interfaces
{
    public interface ITreeBuilder
    {
        IDecisionNode BuildTreeFromDocument(IXmlFileController reader, Action.Action action, string myName);
        IDecisionNode BuildSubTreeFromRepository(IXmlFileController reader, string subtreeName, string subtreeRepositoryPath);
        bool BuildAndSaveXmlDocumentFromTree(IXmlFileController writer, IDecisionNode firstNode, string treeName, string myName);
    }
}