using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulman.WinRT.Helpers
{
    public static class DeviceHelper
    {
        public static string GetDeviceId()
        {
            var packageSpecificToken = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);

            // hardware id, signature, certificate IBuffer objects 
            // that can be accessed through properties.
            var hardwareId = packageSpecificToken.Id;
            var signature = packageSpecificToken.Signature;
            var certificate = packageSpecificToken.Certificate;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            var array = new byte[hardwareId.Length];
            dataReader.ReadBytes(array);
            //string uuid = System.Text.Encoding.UTF8.GetString(array, 0, array.Length);

            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString());
            }            

            return sb.ToString();
        }
    }
}
