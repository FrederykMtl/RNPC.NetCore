using System.Runtime.CompilerServices;
using System.Xml;

[assembly: InternalsVisibleTo("RNPC.API")]

namespace RNPC.Core.Interfaces
{
    public interface IXmlFileController
    {
        XmlDocument LoadFileContent(string characterName, string treeToLoad);
        XmlDocument LoadFileContent(string documentPath);
        XmlDocument LoadNodeSubstitutionsFile(string filePath);
        bool WriteDecisionTreeToXmlFile(string characterName, XmlDocument document);
        bool InitializeCharacterDecisionTrees(string characterName, bool resetDecisionTrees);
    }
}