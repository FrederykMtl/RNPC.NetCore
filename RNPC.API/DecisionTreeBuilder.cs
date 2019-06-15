using System;
using System.IO;
using System.Linq;
using System.Xml;
using RNPC.Core.DecisionTrees;
using RNPC.Core.Exceptions;
using RNPC.Core.Interfaces;

namespace RNPC.API
{
    /// <summary>
    /// 
    /// </summary>
    public class DecisionTreeBuilder : ITreeBuilder
    {
        private IXmlFileController _reader;
        private string _characterName;
        private Core.Action.Action _currentAction;
        
        public IDecisionNode BuildTreeFromDocument(IXmlFileController reader, Core.Action.Action action, string myName)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (reader == null)
                throw new RnpcParameterException("Objects FileController has not been properly initialized.");

            _reader = reader;
            _characterName = myName;
            _currentAction = action;

            string treeName = $"{action.ActionType}-{action.Intent}-{action.EventName}";

            var element = GetXmlElementForAction(treeName);

            var defaultreaction = string.Empty;
                
            if(element?.Attributes["defaultreaction"] != null)
                defaultreaction = element.Attributes["defaultreaction"].Value;

            var rootNode = InitializeNodeWithXmlElement(element?.FirstChild, null, defaultreaction);

            return rootNode;
        }

        /// <summary>
        /// Reads an xml file for a subtree and builds a decision node structure from it.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="subtreeName"></param>
        /// <param name="subtreeRepositoryPath"></param>
        /// <returns></returns>
        public IDecisionNode BuildSubTreeFromRepository(IXmlFileController reader, string subtreeName, string subtreeRepositoryPath)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (reader == null)
                throw new RnpcParameterException("Objects FileController has not been properly initialized.");

            _reader = reader;

            var element = GetXmlElementForAction(subtreeName, subtreeRepositoryPath);

            var rootNode = InitializeNodeWithXmlElement(element.FirstChild, null, string.Empty);

            return rootNode;
        }

        /// <summary>
        /// Builds an XML document from a decision tree, starting with the root node.
        /// </summary>
        /// <param name="writer">file writer</param>
        /// <param name="rootNode">The root of the tree</param>
        /// <param name="decisionTreeName">The name of the decision tree</param>
        /// <param name="myName">Character name</param>
        /// <returns>An xmldocument with the complete decision tree</returns>
        public bool BuildAndSaveXmlDocumentFromTree(IXmlFileController writer, IDecisionNode rootNode, string decisionTreeName, string myName)
        {
            XmlDocument document = new XmlDocument();

            var declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.InsertBefore(declaration, document.DocumentElement);

            XmlNode decisionTree = document.CreateElement(decisionTreeName);
            document.AppendChild(decisionTree);

            XmlNode root = document.CreateElement("Root");
            var name = document.CreateAttribute("text");
            name.Value = rootNode.ToString().Replace("RNPC.DecisionNodes.", "").Replace("RNPC.DecisionLeaves.", "");
            root.Attributes?.Append(name);

            //If it's not a tree with only a leaf
            var branchNode = rootNode as AbstractDecisionNode;
            if (branchNode != null)
            {
                if (branchNode.ConfiguredPassFailValue != 0)
                {
                    var testValue = document.CreateAttribute("test");
                    testValue.Value = branchNode.ConfiguredPassFailValue.ToString();
                    root.Attributes?.Append(testValue);
                }

                if (!string.IsNullOrEmpty(branchNode.DefaultTreeReaction))
                {
                    var defaultReaction = document.CreateAttribute("defaultreaction");
                    defaultReaction.Value = branchNode.DefaultTreeReaction;
                    decisionTree.Attributes?.Append(defaultReaction);
                }

                if (branchNode.LeftNode != null)
                    root.AppendChild(CreateElementFromNode(branchNode.LeftNode, document, "left"));

                if (branchNode.RightNode != null)
                    root.AppendChild(CreateElementFromNode(branchNode.RightNode, document, "right"));
            }

            decisionTree.AppendChild(root);
            writer.WriteDecisionTreeToXmlFile(myName, document);

            return true;
        }

        /// <summary>
        /// Takes a node and creates an XML element
        /// </summary>
        /// <param name="node"></param>
        /// <param name="document"></param>
        /// <param name="decision"></param>
        /// <returns></returns>
        private static XmlNode CreateElementFromNode(IDecisionNode node, XmlDocument document, string decision)
        {
            string nodeName;

            var abstractNode = node as AbstractDecisionNode;

            if (abstractNode != null)
                nodeName = "Node";
            else
                nodeName = "Leaf";

            XmlNode newNode = document.CreateElement(nodeName);

            var name = document.CreateAttribute("text");
            name.Value = node.ToString().Replace("RNPC.DecisionNodes.", "").Replace("RNPC.DecisionLeaves.", "");
            newNode.Attributes?.Append(name);

            AddDecisionAttribute(document, decision, newNode);
            AddTestAttribute(node, document, newNode);

            if (abstractNode == null)
                return newNode;

            if (abstractNode.LeftNode != null)
            {
                newNode.AppendChild(CreateElementFromNode(abstractNode.LeftNode, document, "left"));
            }

            if (abstractNode.RightNode != null)
            {
                newNode.AppendChild(CreateElementFromNode(abstractNode.RightNode, document, "right"));
            }

            return newNode;
        }

        private static void AddDecisionAttribute(XmlDocument document, string decision, XmlNode newNode)
        {
            if (string.IsNullOrWhiteSpace(decision))
                return;

            //leaves room for a third possibility
            switch (decision.ToLower())
            {
                case "left":
                    var leftAttr = document.CreateAttribute("decision");
                    leftAttr.Value = "left";
                    newNode.Attributes?.Append(leftAttr);
                    break;
                case "right":
                    var rightAttr = document.CreateAttribute("decision");
                    rightAttr.Value = "right";
                    newNode.Attributes?.Append(rightAttr);
                    break;
                default:
                    throw new ArgumentException("Decision argument unrecognized");
            }
        }

        /// <summary>
        /// Adds the test value attribute
        /// </summary>
        /// <param name="node"></param>
        /// <param name="document"></param>
        /// <param name="newNode"></param>
        private static void AddTestAttribute(IDecisionNode node, XmlDocument document, XmlNode newNode)
        {
            var abstractNode = node as AbstractDecisionNode;

            if (abstractNode == null || abstractNode.ConfiguredPassFailValue == 0)
                return;

            var testValue = document.CreateAttribute("test");
            testValue.Value = abstractNode.ConfiguredPassFailValue.ToString();
            newNode.Attributes?.Append(testValue);
        }

        /// <summary>
        /// Gets xml from document
        /// </summary>
        /// <param name="treeName"></param>
        /// <returns></returns>
        private XmlElement GetXmlElementForAction(string treeName)
        {
            var treeDocument = _reader.LoadFileContent(_characterName, treeName);

            if (treeDocument == null)
                throw new XmlException("The treeDocument has not been properly loaded.");

            var element = treeDocument.DocumentElement;

            if (element?.FirstChild == null || element.Name != treeName)
                throw new XmlException("The treeDocument struture has not been properly read from file or the structure is invalid.");

            return element;
        }

        /// <summary>
        /// Gets xml from document
        /// </summary>
        /// <param name="treeName"></param>
        /// <param name="xmlDocumentLocation">Directory where all the ubtree files are contained</param>
        /// <returns></returns>
        private XmlElement GetXmlElementForAction(string treeName, string xmlDocumentLocation)
        {
            var treeDocument = _reader.LoadFileContent(xmlDocumentLocation + treeName + ".xml");

            if (treeDocument == null)
                throw new XmlException("The treeDocument has not been properly loaded.");

            var element = treeDocument.DocumentElement;

            if (element?.FirstChild == null || element.Name != treeName)
                throw new XmlException("The treeDocument struture has not been properly read from file or the structure is invalid.");

            return element;
        }

        /// <summary>
        /// Uses information found in an element to initialize the values of a decision node
        /// </summary>
        /// <param name="element">Current node to initialize</param>
        /// <param name="parent">Parent node</param>
        /// <param name="defaultReaction">The default reaction for the tree being built</param>
        /// <returns></returns>
        private IDecisionNode InitializeNodeWithXmlElement(XmlNode element, IDecisionNode parent, string defaultReaction)
        {
            if (element.Attributes == null || element.Attributes.Count == 0)
                throw new NodeInitializationException($"Element attributes for node {element.Name} are empty");

            var nodeName = element.Attributes["text"]?.Value;

            if(nodeName == null)
                throw new NodeInitializationException($"Text attribute for node {element.Name} is empty");

            var invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            nodeName = invalid.Aggregate(nodeName, (current, c) => current.Replace(c.ToString(), string.Empty)).Replace(" ", string.Empty);

            //We have a subtree
            if (element.Attributes["type"] != null && element.Attributes["type"].Value == "subtree")
            {
                //conditional load of a subtree. Minimizes unnecessary loads
                if (element.Attributes["keyword"] != null)
                {
                    var keywords = element.Attributes["keyword"].Value.ToLower();
                    //if none of the keyword is found
                    if (keywords.Split(',').Any(keyword => !_currentAction.Message.ToLower().Contains(keyword)))
                    {
                        return null;
                    }
                }
                var subElement = GetXmlElementForAction(nodeName);

                var rootNode = InitializeNodeWithXmlElement(subElement.FirstChild, null, defaultReaction);

                return rootNode;
            }

            //Leaf
            if (element.Name.ToLower() == "leaf")
            {
                if (nodeName.ToLower() == "default" && !string.IsNullOrEmpty(defaultReaction))
                    nodeName = defaultReaction;

                var leaf = FindLeafClassByReflection(nodeName);

                if (leaf == null)
                {
                    throw new NullNodeException("A tree leaf could not be found in class DecisionTreeBuilder. Check that the class associated with the tree exists.");
                }

                leaf.SetParentNode(parent);

                return leaf;
            }

            //Regular node
            var node = FindNodeClassByReflection(nodeName);

            if (node == null)
                return null;

            //is this the root node?
            if (parent == null)
                node.DefaultTreeReaction = defaultReaction; 
            else
                node.SetParentNode(parent);

            if (element.Attributes["test"] != null)
            {
                var testValue = int.Parse(element.Attributes["test"].Value);
                node.ConfiguredPassFailValue = testValue;
            }

            if (!element.HasChildNodes || element.FirstChild?.Attributes == null)
                return node;

            switch (element.FirstChild.Attributes["decision"]?.Value)
            {
                case "left":
                    node.LeftNode = InitializeNodeWithXmlElement(element.FirstChild, node, defaultReaction);

                    if (element.ChildNodes.Count > 1)
                    {
                        node.RightNode = InitializeNodeWithXmlElement(element.ChildNodes[1], node, defaultReaction);
                    }
                    break;
                case "right":
                    node.RightNode = InitializeNodeWithXmlElement(element.FirstChild, node, defaultReaction);

                    if (element.ChildNodes.Count > 1)
                    {
                        node.LeftNode = InitializeNodeWithXmlElement(element.ChildNodes[1], node, defaultReaction);
                    }
                    break;
                default:
                    throw new NodeInitializationException("Decision attribute set to an unknown value");
            }

            return node;
        }

        private static AbstractDecisionNode FindNodeClassByReflection(string className)
        {            
            var type = Type.GetType("RNPC.API.DecisionNodes." + className);

            if (type == null)
                return null;

            return (AbstractDecisionNode)Activator.CreateInstance(type);
        }

        private static IDecisionNode FindLeafClassByReflection(string className)
        {
            var type = Type.GetType("RNPC.API.DecisionLeaves." + className);

            if (type == null)
                return null;

            return (IDecisionNode)Activator.CreateInstance(type);
        }
    }
}