﻿using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kulman.WinRT.Extensions
{
    public static class FolderExtensions
    {       
        /// <summary>
        /// Checks if a folder contains a file
        /// </summary>
        /// <param name="folder">Folder</param>
        /// <param name="localFilename">Filename</param>
        /// <returns>True if the folder contains the file</returns>
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
