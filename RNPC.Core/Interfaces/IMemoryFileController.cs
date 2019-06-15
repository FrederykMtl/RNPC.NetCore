using System;

namespace RNPC.Core.Interfaces
{
    public interface IMemoryFileController
    {
        void WriteToFile(Guid characterId, Memory.Memory memoriesToSave, bool saveMemoryBackup = false);
        Memory.Memory ReadFromFile(Guid characterId, bool useMemoryBackupAsFailsafe = false);
        void DeleteFile(Guid characterId, bool deleteMemoryBackup = false);
        bool FileExists(Guid characterId);
    }
}