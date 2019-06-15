using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MBrace.FsPickler;
using RNPC.Core.Exceptions;
using RNPC.Core.Interfaces;
using RNPC.Core.Memory;
using RNPC.Core.Resources;

namespace RNPC.FileManager
{
    public class OmniscienceFileController : IOmniscienceFileController
    {
        /// <summary>
        /// Directory where the all knowledge file will be stored
        /// </summary>
        private readonly string _omniscienceDirectory;

        /// <summary>
        /// Default Constructor. Will create the directory if it's non-existent.
        /// </summary>
        /// 
        public OmniscienceFileController()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationDirectory.Instance.KnowledgeFilesDirectory))
                throw new RnpcParameterException("A file path is required for the saving of the Omniscience file.", new Exception("Missing configuredPath"));

            _omniscienceDirectory = ConfigurationDirectory.Instance.KnowledgeFilesDirectory;

            if (!Directory.Exists(_omniscienceDirectory))
            {
                Directory.CreateDirectory(_omniscienceDirectory);
            }
        }

        /// <summary>
        /// This write the content of the all-knowledge to file on disk
        /// </summary>
        /// <param name="knowledgeToSave">knowledge class with content</param>
        /// <param name="saveOmniscienceBackup">use backup in case of error</param>
        public void WriteToFile(Omniscience knowledgeToSave, bool saveOmniscienceBackup = false)
        {
            var serializer = FsPickler.CreateBinarySerializer();

            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes("ZCR0N0S1"),
                IV = Encoding.ASCII.GetBytes("ZCR0N0S1")
            };

            try
            {
                using (var file = File.Create(GetFilelocation()))
                {
                    CryptoStream crStream = new CryptoStream(file, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
                    serializer.Serialize(crStream, knowledgeToSave);
                    crStream.Close();

                    if (!saveOmniscienceBackup)
                        return;

                    try
                    {
                        using (var backupFile = File.Create(GetBackupFilelocation()))
                        {
                            CryptoStream crBackupStream = new CryptoStream(backupFile, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
                            serializer.Serialize(crBackupStream, knowledgeToSave);
                            crBackupStream.Close();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new RnpcFileAccessException("Error when trying to write the **backup** Omniscience to file.", e);
                    }
                }
            }
            catch (Exception e)
            {
                throw new RnpcFileAccessException("Error when trying to write Omniscience to file.", e);
            }
        }

        /// <summary>
        /// Reads an .rmf file, decrypts and deserializes its content and returns it
        /// </summary>
        /// <param name="useOmniscienceBackupAsFailsafe">Indicates whether the backup file shoud be used in case of read error</param>
        /// <returns>a Omniscience object</returns>
        public Omniscience ReadFromFile(bool useOmniscienceBackupAsFailsafe = false)
        {
            var cryptic = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes("ZCR0N0S1"),
                IV = Encoding.ASCII.GetBytes("ZCR0N0S1")
            };

            try
            {
                FileStream stream = new FileStream(GetFilelocation(), FileMode.Open, FileAccess.Read);

                var serializer = FsPickler.CreateBinarySerializer();
                var crStream = new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

                return serializer.Deserialize<Omniscience>(crStream);
            }
            catch (Exception e)
            {
                if (!useOmniscienceBackupAsFailsafe)
                    throw new RnpcFileAccessException("Error when trying to read Omniscience file.", e);

                try
                {
                    FileStream stream = new FileStream(GetBackupFilelocation(), FileMode.Open, FileAccess.Read);

                    var serializer = FsPickler.CreateBinarySerializer();
                    var crStream = new CryptoStream(stream, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

                    return serializer.Deserialize<Omniscience>(crStream);
                }
                catch (Exception ex)
                {
                    throw new RnpcFileAccessException("Error when trying to read Omniscience file and backup Omniscience file!", ex);
                }

            }
        }

        /// <summary>
        /// Delete a character Omniscience file
        /// </summary>
        /// <param name="deleteOmniscienceBackup">Indicates if you want the backup file to be deleted.</param>
        public void DeleteFile(bool deleteOmniscienceBackup = false)
        {
            var fileLocation = GetFilelocation();

            try
            {
                if (File.Exists(fileLocation))
                    File.Delete(fileLocation);
            }
            catch (Exception e)
            {
                throw new RnpcFileAccessException("Error when trying to delete Omniscience file.", e);
            }
        }

        /// <summary>
        /// Returns whether the Omniscience file exists, where it should be.
        /// </summary>
        /// <returns>True if file exists, false if not found</returns>
        public bool FileExists()
        {
            return File.Exists(GetFilelocation());
        }

        #region private methods

        /// <summary>
        /// Establishes where the file will be found based on current configured path
        /// </summary>
        /// <returns>file's path</returns>
        private string GetFilelocation()
        {
            return _omniscienceDirectory + "416c6c2d4b6e6f776c65646765.rmf";
        }

        /// <summary>
        /// Establishes where the backup file will be found based on current configured path
        /// </summary>
        /// <returns>file's path</returns>
        private string GetBackupFilelocation()
        {
            return _omniscienceDirectory + "416c6c2d4b6e6f776c65646765.bmf";
        }
        #endregion
    }
}