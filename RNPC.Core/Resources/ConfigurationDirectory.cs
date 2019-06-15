namespace RNPC.Core.Resources
{
    public class ConfigurationDirectory
    {
        private static ConfigurationDirectory _instance;
        public static ConfigurationDirectory Instance => _instance ?? (_instance = new ConfigurationDirectory());

        public string CharacterFilesDirectory;
        public string NodeSubstitutionsFile;
        public string CentralDecisionTreeRepository;
        public string SubTreeRepository;
        public string LogFilesDirectory;
        public string KnowledgeFilesDirectory;

        private ConfigurationDirectory()
        {
        }
    }
}
