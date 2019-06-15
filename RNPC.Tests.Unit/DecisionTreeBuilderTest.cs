using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.API;
using RNPC.Core.Action;
using RNPC.Core.Enums;
using RNPC.Core.Exceptions;
using RNPC.Tests.Unit.Stubs;

namespace RNPC.Tests.Unit
{
    [TestClass]
    public class DecisionTreeBuilderTest
    {
        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void BuildTreeFromDocument_EmptyTree_ExceptionRaised()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            //ACT
            builder.BuildTreeFromDocument(new XmlStub(), GetInsult("Rick"), "empty");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException), "The treeDocument has not been properly loaded.")]
        public void BuildTreeFromDocument_NoXMlDocument_ExceptionRaised()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            //ACT
            builder.BuildTreeFromDocument(new XmlStub(), GetInsult("Rick"), "nodoc");
        }

        [TestMethod]
        public void BuildTreeFromDocument_ValidTree_ValidResult()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();

            //ACT
            var result = builder.BuildTreeFromDocument(new XmlStub(), GetInsult("Rick"), "valid");

            //ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeInitializationException))]
        public void BuildTreeFromDocument_NoAttributeInElement_ExceptionRaised()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();
            //XmlDocument document = LoadTestFileContent("C:\\Sysdev\\RNPC\\RNPC\\RNPCUnitTests\\bin\\TestFileNoAttribute.xml");

            //ACT
            builder.BuildTreeFromDocument(new XmlStub(), GetInsult("Rick"), "noattrele");
        }

        [TestMethod]
        [ExpectedException(typeof(NodeInitializationException))]
        public void BuildTreeFromDocument_NoTextAttributes_ExceptionRaised()
        {
            //ARRANGE
            DecisionTreeBuilder builder = new DecisionTreeBuilder();
            //ACT
            builder.BuildTreeFromDocument(new XmlStub(), GetInsult("Rick"), "notext");
        }

        public XmlDocument LoadTestFileContent(string path)
        {
            XmlDocument document = new XmlDocument();

            var location = path;

            if (File.Exists(location))
                document.Load(location);
            else
                return null;

            return document;
        }

        private Action GetInsult(string myName)
        {
            return new Action
            {
                EventType = EventType.Interaction,
                ActionType = ActionType.Verbal,
                Intent = Intent.Hostile,
                Message = "You're really dumb.",
                Target = myName,
                EventName = "Insult",
                Source = "The Frenemy"
            };
        }
    }
}