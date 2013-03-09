using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Kulman.WinRT.Helpers
{
    public static class WebHelper
    {
        public static bool IsConnectedToInternet()
        {
            //return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            return (connectionProfile != null &&
                    connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }


        public static async Task<string> SendPostToUriAsync(string targetUri, string content)
        {
            var handler = new HttpClientHandler {UseDefaultCredentials = true, AllowAutoRedirect = false};

            var client = new HttpClient(handler);

            HttpContent httpContent = new StringContent(content);
            //Debug.WriteLine(content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


            HttpResponseMessage response = await client.PostAsync(targetUri, httpContent);


            return await response.Content.ReadAsStringAsync();
        }


        public static async Task<string> DownloadPageAsync(string url)
        {
            var handler = new HttpClientHandler {UseDefaultCredentials = true, AllowAutoRedirect = true};
            var client = new HttpClient(handler) {MaxResponseContentBufferSize = 196608};
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public static async Task<string> DownloadPageAsync(string url, string encoding)
        {
            var handler = new HttpClientHandler {UseDefaultCredentials = true, AllowAutoRedirect = true};
            var client = new HttpClient(handler) {MaxResponseContentBufferSize = 196608};
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var arr = await response.Content.ReadAsByteArrayAsync();

            var myEncoding = new CustomEncoding();

            return myEncoding.GetString(arr, 0, arr.Length);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTags(this string source)
        {
            var array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}