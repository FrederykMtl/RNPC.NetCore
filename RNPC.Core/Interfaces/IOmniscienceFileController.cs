using RNPC.Core.Memory;

namespace RNPC.Core.Interfaces
{
    public interface IOmniscienceFileController
    {
        void WriteToFile(Omniscience knowledgeToSave, bool saveOmniscienceBackup = false);
        Omniscience ReadFromFile(bool useOmniscienceBackupAsFailsafe = false);
        void DeleteFile(bool deleteOmniscienceBackup = false);
        bool FileExists();
    }
}