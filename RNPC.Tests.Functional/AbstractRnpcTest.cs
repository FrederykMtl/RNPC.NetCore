using RNPC.Core.Resources;

namespace RNPC.Tests.Functional
{
    public class AbstractRnpcTest
    {
        public AbstractRnpcTest()
        {
            ConfigurationDirectory.Instance.NodeSubstitutionsFile = @"..\\..\\..\\..\\RNPC.Core\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            ConfigurationDirectory.Instance.CentralDecisionTreeRepository = @"..\\..\\..\\..\\RNPC.API\\XMLTreeFiles\\";
            ConfigurationDirectory.Instance.SubTreeRepository = @"..\\..\\..\\..\\RNPC.API\\Subtrees\\";
            ConfigurationDirectory.Instance.CharacterFilesDirectory = @"C:\Sysdev\RNPC\logs\Characters\";
            ConfigurationDirectory.Instance.KnowledgeFilesDirectory = @"C:\Sysdev\RNPC\Knowledge\";
            ConfigurationDirectory.Instance.LogFilesDirectory = @"C:\Sysdev\RNPC\logs\";
            //ConfigurationDirectory.Instance.CharacterFilesDirectory = "filepath";
            //ConfigurationDirectory.Instance.NodeSubstitutionsFile = @"C:\\Sysdev\\RNPC\\RNPC.NetCore\\RNPC.Core\\Learning\\Resources\\DecisionTreeSubstitutions.xml";
            //ConfigurationDirectory.Instance.CentralDecisionTreeRepository = @"./SubTreeSubstitionMap.xml";
            //ConfigurationDirectory.Instance.LogFilesDirectory = "./Log";
            //ConfigurationDirectory.Instance.KnowledgeFilesDirectory = @"./knwldg";
        }
    }
}