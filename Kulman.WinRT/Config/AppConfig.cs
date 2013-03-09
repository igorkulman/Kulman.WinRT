using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Kulman.WinRT.Config
{
    public static class AppConfig
    {
        #region Fields
        private static XDocument _document;
        private static readonly Dictionary<string, string> Dictionary = new Dictionary<string, string>();
        #endregion

     

        public async static Task<bool> ParseAndLoadAppConfig()
        {
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"config.xml");           
            using (var fs = await file.OpenAsync(FileAccessMode.Read))
            {
                using (var inStream = fs.GetInputStreamAt(0))
                {
                    using (var reader = new DataReader(inStream))
                    {
                        await reader.LoadAsync((uint)fs.Size);
                        string data = reader.ReadString((uint)fs.Size);
                        reader.DetachStream();
                        _document = XDocument.Parse(data, LoadOptions.None);
                    }
                }
            }

            if (!_document.Elements().Any())
            {
                //MessageBox.Show("No config file");
                return false;
            }

            try
            {


                var root = _document.Root;
                if (root == null)
                {
                    //MessageBox.Show("No root element");
                    return false;
                }

                foreach (var element in root.Elements().Where(x => x.Name.LocalName == "add"))
                {
                    if (!Dictionary.ContainsKey(element.Attribute("key").Value))
                    {
                        Dictionary.Add(element.Attribute("key").Value, element.Value.Trim());
                    }

                    if (string.IsNullOrEmpty(element.Value))
                    {
                        throw new Exception("Null value");
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                Debug.WriteLine(e.ToString());
                return false;
            }

            return true;
        }

        public static Dictionary<string, string> Settings
        {
            get
            {               
                return Dictionary;
            }
        }

    }
}
