using System.Text;
using Windows.Storage;

namespace Kulman.WinRT.Helpers
{
    public static class DeviceHelper
    {
        public static string GetDeviceId()
        {
            var saved = ApplicationData.Current.LocalSettings.Values["DeviceId"];
            if (saved != null)
            {
                return (string) saved;
            }

            var packageSpecificToken = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);

            // hardware id, signature, certificate IBuffer objects 
            // that can be accessed through properties.
            var hardwareId = packageSpecificToken.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            var array = new byte[hardwareId.Length];
            dataReader.ReadBytes(array);
            //string uuid = System.Text.Encoding.UTF8.GetString(array, 0, array.Length);

            var sb = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString());
            }

            ApplicationData.Current.LocalSettings.Values["DeviceId"] = sb.ToString();
            return sb.ToString();
        }
    }
}