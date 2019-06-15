using Microsoft.VisualStudio.TestTools.UnitTesting;
using RNPC.Core.Exceptions;
using RNPC.FileManager;

namespace RNPC.Tests.Unit.KnowledgeFilesManager
{
    [TestClass]
    public class OmniscienceFileControllerTest
    {
        [TestMethod]
        [ExpectedException(typeof(RnpcParameterException))]
        public void New_ConstructorCallWithEmptyPath_ExceptionRaised()
        {
            //ARRANGE
            //ACT
            OmniscienceFileController controller = new OmniscienceFileController();
        }

        [TestMethod]
        public void WriteToFile__()
        {
        }
    }
}
