using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MBrace.FsPickler;
using RNPC.Core.Exceptions;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;
using RNPC.FileManager.Resources;

namespace RNPC.FileManager
{
    public class MemoryFileController : IMemoryFileController
    {
        /// <summary>
        /// Directory where the all memory file will be stored
        /// </summary>
        private readonly string _memoryDirectory;

        /// <summary>
        /// Default Constructor. Will create the directory if it's non-existent.
        /// </summary>
        /// <param name="configuredPath"></param>
        public MemoryFileController(string configuredPath)
        {
            if (string.IsNullOrWhiteSpace(configuredPath))
                throw new RnpcParameterException("A file path is required for the saving of the memory file.", new Exception("Missing configuredPath"));

            var directoryResource = ApplicationDirectories.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true);

            string directory = directoryResource.GetString("Memories");

            _memoryDirectory = configuredPath + directory + "\\";

            if (!Directory.Exists(_memoryDirectory))
            {
                Directory.CreateDirectory(_memoryDirectory);
            }
        }

        /// <summary>
        /// This write the content of a characters memories to file on disk
        /// Includes: Short term &  Long term memories, opinions and node results
        /// </summary>
        /// <param name="characterId">The name of the character to save. Important for file location and name</param>
        /// <param name="memoriesToSave">The character's Memory object</param>
        /// <param name="saveMemoryBackup">indicates if a backup of the memory file should be made</param>
        public void WriteToFile(Guid characterId, Memory memoriesToSave, bool saveMemoryBackup = false)
        {
            try
            {
                using (var file = File.Create(GetFilelocation(characterId)))
                {
                    CryptoStream crStream = CreateEncryptedStream(file);
                    
                    var serializer = FsPickler.CreateBinarySerializer();
                    serializer.Serialize(crStream, memoriesToSave);

                    crStream.Close();

                    if (!saveMemoryBackup) return;

                    using (var backupFile = File.Create(GetBackupFilelocation(characterId)))
                    {
                        CryptoStream crBackupStream = CreateEncryptedStream(backupFile);
                        serializer.Serialize(crBackupStream, memoriesToSave);
                        crBackupStream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new RnpcFileAccessException("Error when trying to write Memory to file.", e);
            }
        }

        /// <summary>
        /// Reads an .rmf file, decrypts and deserializes its content and returns it
        /// </summary>
        /// <param name="characterId">Name of the character</param>
        /// <param name="useMemoryBackupAsFailsafe">Indicates whether the backup file shoud be used in case of read error</param>
        /// <returns>a memory object</returns>
        public Memory ReadFromFile(Guid characterId, bool useMemoryBackupAsFailsafe = false)
        {
            try
            {
                FileStream stream = new FileStream(GetFilelocation(characterId), FileMode.Open, FileAccess.Read);

                var serializer = FsPickler.CreateBinarySerializer();

                var crStream = CreateDecryptingStream(stream);

                return serializer.Deserialize<Memory>(crStream);
            }
            catch (Exception e)
            {
                if (!useMemoryBackupAsFailsafe)
                    throw new RnpcFileAccessException("Error when trying to read Memory file.", e);

                try
                {
                    FileStream stream = new FileStream(GetBackupFilelocation(characterId), FileMode.Open, FileAccess.Read);

                    var serializer = FsPickler.CreateBinarySerializer();

                    var crStream = CreateDecryptingStream(stream);

                    return serializer.Deserialize<Memory>(crStream);
                }
                catch (Exception ex)
                {
                    throw new RnpcFileAccessException("Error when trying to read Memory file and backup Memory file!", ex);
                }
            }
}

        /// <summary>
        /// Delete a character memory file
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="deleteMemoryBackup">Indicates if you want the backup file to be deleted.</param>
        public void DeleteFile(Guid characterId, bool deleteMemoryBackup = false)
        {
            var fileLocation = GetFilelocation(characterId);

            try
            {
                if (File.Exists(fileLocation))
                    File.Delete(fileLocation);
            }
            catch (Exception e)
            {
                throw new RnpcFileAccessException("Error when trying to delete Memory file.", e);
            }
        }

        /// <summary>
        /// Returns whether the Memory file exists, where it should be.
        /// </summary>
        /// <returns>True if file exists, false if not found</returns>
        public bool FileExists(Guid characterId)
        {
            return File.Exists(GetFilelocation(characterId));
        }

        #region private methods

        /// <summary>
        /// Establishes where the file will be found based on parameters
        /// </summary>
        /// <param name="characterId">Character's name - used for the Directory of the file</param>
        /// <returns>file's path</returns>
        private string GetFilelocation(Guid characterId)
        {
            if (characterId == Guid.Empty)
                return null;

            return _memoryDirectory + "\\" + characterId + ".rmf";
        }

        /// <summary>
        /// Establishes where the file will be found based on parameters
        /// </summary>
        /// <param name="characterId">Character's name - used for the Directory of the file</param>
        /// <returns>file's path</returns>
        private string GetBackupFilelocation(Guid characterId)
        {
            if (characterId == Guid.Empty)
                return null;

            return _memoryDirectory + "\\" + characterId + ".bmf";
        }

        private static CryptoStream CreateEncryptedStream(FileStream file)
        {
            var cryptic = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes("CPNRRNPC"),
                IV = Encoding.ASCII.GetBytes("CPNRRNPC")
            };

            return new CryptoStream(file, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
        }

        private static CryptoStream CreateDecryptingStream(FileStream stream)
        {
            var cryptic = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes("CPNRRNPC"),
                IV = Encoding.ASCII.GetBytes("CPNRRNPC")
            };

            return new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);
        }
        #endregion
    }
}