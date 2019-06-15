using RNPC.FileManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Interfaces;
using RNPC.Core.Learning.Substitutions;
using RNPC.Core.Resources;

namespace RNPC.Tests.Functional.Learning
{
    /// <summary>
    /// Summary description for SubstitutionDocumentConverterTest
    /// </summary>
    [TestClass]
    public class SubstitutionDocumentConverterTest : AbstractFunctionalTest
    {       
        [TestMethod]
        public void ConvertToDocumentToList_ValidDocument_ListOfSubstitutionsReturned()
        {
            //ARRANGE
            SubstitutionDocumentConverter converter = new SubstitutionDocumentConverter();
            IXmlFileController controller = new DecisionTreeFileController();
            var filePath = controller.LoadNodeSubstitutionsFile(ConfigurationDirectory.Instance.NodeSubstitutionsFile);

            //ACT
            var result = converter.ConvertDocumentToList(filePath);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
