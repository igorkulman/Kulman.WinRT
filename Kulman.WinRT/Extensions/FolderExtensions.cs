using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kulman.WinRT.Extensions
{
    public static class FolderExtensions
    {
       

        public static async Task<bool> ContainsFileAsync(this StorageFolder folder,string localFilename)
        {
            //bool exists = await ApplicationData.Current.LocalFolder.ContainsFileAsync(localFilename);
            //return exists;
            try
            {
                var f = await folder.GetFileAsync(localFilename);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
