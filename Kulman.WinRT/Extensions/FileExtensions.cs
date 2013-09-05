using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;

namespace Kulman.WinRT.Extensions
{
    public static class FileExtensions
    {
        /// <summary>
        /// Gets a file bundled in the applications installation
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <param name="fileName">File name</param>
        /// <returns>File</returns>
        public static async Task<StorageFile> GetPackagedFile(string folderName, string fileName)
        {
            var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            if (folderName != null)
            {
                StorageFolder subFolder = await installFolder.GetFolderAsync(folderName);
                return await subFolder.GetFileAsync(fileName);
            }
            return await installFolder.GetFileAsync(fileName);
        }

        /// <summary>
        /// Gets the MD5 checksum of a file in ApplicationData.Current.LocalFolder
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>MD5 checksum</returns>
        public static async Task<string> GetMD5Checksum(string filename)
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            var s = await file.OpenReadAsync();
            var fs = s.AsStreamForRead();

            var bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);

            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var hashed = alg.HashData(bytes.AsBuffer());
            var res = CryptographicBuffer.EncodeToBase64String(hashed);
            return res;
        }

        /// <summary>
        /// Gets the MD5 checksum of a file
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>MD5 checksum</returns>
        public static async Task<string> GetMD5Checksum(StorageFile file)
        {
            var s = await file.OpenReadAsync();
            var fs = s.AsStreamForRead();

            var bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);

            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var hashed = alg.HashData(bytes.AsBuffer());
            var res = CryptographicBuffer.EncodeToBase64String(hashed);
            return res;
        }
    }
}