using System.IO;
using System.Xml;
using RNPC.Core.Interfaces;
using RNPC.Core.Resources;

namespace RNPC.FileManager
{
    public class DecisionTreeFileController : IXmlFileController
    {
        /// <summary>
        /// Loads an decision tree file into memory
        /// </summary>
        /// <param name="characterName">Name of the character - decision trees are personalized</param>
        /// <param name="treeToLoad">Name of the tree to load. Standard is InteractionType-Intent-Action
        /// Ex.: Verbal-Hostile-Insult</param>
        /// <returns>Xml document with the tree</returns>
        public XmlDocument LoadFileContent(string characterName, string treeToLoad)
        {
            XmlDocument document = new XmlDocument();

            var location = GetFilelocation(treeToLoad, characterName);

            if(File.Exists(location))
                document.Load(location);
            else
                return null;

            return document;
        }

        /// <summary>
        /// Loads an decision tree file into memory
        /// </summary>
        /// <param name="documentPath">Qualified path of the file</param>
        /// <returns>Xml document with the tree</returns>
        public XmlDocument LoadFileContent(string documentPath)
        {
            XmlDocument document = new XmlDocument();

            if (File.Exists(documentPath))
                document.Load(documentPath);
            else
                return null;

            return document;
        }

        /// <summary>
        /// Establishes where the file will be found based on parameters
        /// </summary>
        /// <param name="treeToLoad">Name of the decision tree, based to establish file name</param>
        /// <param name="characterName">Character's name - used for the Directory of the file</param>
        /// <returns></returns>
        private static string GetFilelocation(string treeToLoad, string characterName)
        {
            if (string.IsNullOrWhiteSpace(treeToLoad) || string.IsNullOrWhiteSpace(characterName))
                return null;

            //var directoryResource = ApplicationDirectories.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            //string directory = directoryResource.GetString("DecisionTree");

            string file = treeToLoad;

            if (!treeToLoad.Contains(".xml"))
                file = treeToLoad + ".xml";

            return ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName + "\\decisiontrees\\" + file;
        }

        public XmlDocument LoadNodeSubstitutionsFile(string filePath)
        {
            XmlDocument document = new XmlDocument();

            if (File.Exists(filePath))
                document.Load(filePath);
            else
                return null;

            return document;
        }

        /// <summary>
        /// Takes care of writing an xml document to file 
        /// </summary>
        /// <param name="characterName"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public bool WriteDecisionTreeToXmlFile(string characterName, XmlDocument document)
        {
            document.Save(ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName + "\\DecisionTrees\\" + document.DocumentElement?.Name + ".xml");
            return true;
        }

        /// <summary>
        /// Will check if the Character's directory exists, will create it if necessary and add decision trees 
        /// </summary>
        /// <param name="characterName">Name of the new Character</param>
        /// <param name="resetDecisionTrees">Whether we should overwrite existing trees</param>
        /// <returns></returns>
        public bool InitializeCharacterDecisionTrees(string characterName, bool resetDecisionTrees)
        {
            string characterDecisionTreesPath = ConfigurationDirectory.Instance.CharacterFilesDirectory + characterName + "\\decisiontrees\\";

            if (!Directory.Exists(characterDecisionTreesPath))
            {
                Directory.CreateDirectory(characterDecisionTreesPath);
            }

            //if there are no decision trees we will copy them
            if (Directory.GetFiles(characterDecisionTreesPath).Length == 0 || resetDecisionTrees)
            {
                CopyFilesToDirectory(characterDecisionTreesPath, true);
            }

            return true;
        }

        private static void CopyFilesToDirectory(string path, bool overwrite)
        {
            foreach (var treeFile in Directory.EnumerateFiles(ConfigurationDirectory.Instance.CentralDecisionTreeRepository))
            {
                string fileName = Path.GetFileName(treeFile);

                if (!string.IsNullOrEmpty(fileName))
                {
                    File.Copy(treeFile, path + fileName, overwrite);
                }
            }
        }
    }
}
