using System.Xml;
using RNPC.Core.Interfaces;

namespace RNPC.Tests.Unit.Stubs
{
    public class XmlStub : IXmlFileController
    {
        public XmlDocument LoadFileContent(string characterName, string treeToLoad)
        {
            string documentText = string.Empty;

            switch (characterName)
            {
                case "nodoc":
                    return null;
                case "empty":
                    documentText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                   "<Verbal-Hostile-Insult>" +
                                   "</Verbal-Hostile-Insult >";
                    break;
                case "valid":
                    documentText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                   "<Verbal-Hostile-Insult>" +
                                   "<Root text=\"Am I In A Bad Mood?\" >" +
                                   "</Root>" +
                                   "</Verbal-Hostile-Insult >";
                    break;
                case "noattrele":
                    documentText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                   "<Verbal-Hostile-Insult>" +
                                   "<Root>" +
                                   "</Root>" +
                                   "</Verbal-Hostile-Insult >";
                    break;
                case "notext":
                    documentText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                   "<Verbal-Hostile-Insult>" +
                                   "<Root name=\"Am I In A Bad Mood?\" >" +
                                   "</Root>" +
                                   "</Verbal-Hostile-Insult >";
                    break;
            }

            var doc = new XmlDocument
            {
                InnerXml = documentText
            };

            return doc;
        }

        public XmlDocument LoadFileContent(string documentPath)
        {
            throw new System.NotImplementedException();
        }

        public XmlDocument LoadNodeSubstitutionsFile(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public bool WriteDecisionTreeToXmlFile(string characterName, XmlDocument document)
        {
            throw new System.NotImplementedException();
        }

        public bool InitializeCharacterDecisionTrees(string characterName, bool resetDecisionTrees)
        {
            throw new System.NotImplementedException();
        }
    }
}
