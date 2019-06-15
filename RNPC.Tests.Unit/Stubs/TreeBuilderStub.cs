using RNPC.Core.Action;
using RNPC.Core.Interfaces;

namespace RNPC.Tests.Unit.Stubs
{
    public class TreeBuilderStub : ITreeBuilder
    {
        public IDecisionNode BuildTreeFromDocument(IXmlFileController reader, Action action, string myName)
        {
            return null;
        }

        public IDecisionNode BuildSubTreeFromRepository(IXmlFileController reader, string subtreeName,
            string subtreeRepositoryPath)
        {
            throw new System.NotImplementedException();
        }

        public bool BuildAndSaveXmlDocumentFromTree(IXmlFileController controller, IDecisionNode firstNode, string treename, string myName)
        {
            throw new System.NotImplementedException();
        }
    }
}