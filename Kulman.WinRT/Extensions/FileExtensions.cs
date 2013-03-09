using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kulman.WinRT.Extensions
{
    public static class FileExtensions
    {
        public static async Task<StorageFile> GetPackagedFile(string folderName, string fileName)
        {
            var installFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            if (folderName != null)
            {
                var subFolder = await installFolder.GetFolderAsync(folderName);
                return await subFolder.GetFileAsync(fileName);
            }
            return await installFolder.GetFileAsync(fileName);
        }
    }
}