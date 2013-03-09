using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kulman.WinRT.Net
{
    public class RestClient
    {
        public class Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        private readonly string _targetUrl;

        public List<Parameter> Parameters { get; private set; }

        public RestClient(string targetUrl)
        {
            _targetUrl = targetUrl;
            Parameters = new List<Parameter>();
        }

        public async Task<string> SendPostToUriAsync(string path)
        {
            var handler = new HttpClientHandler {UseDefaultCredentials = true, AllowAutoRedirect = false};

            var client = new HttpClient(handler);

            var sb = new StringBuilder();
            for (int i=0; i<Parameters.Count;++i)
            {
                if (Parameters[i] == null || Parameters[i].Name == null || Parameters[i].Value == null) continue;

                sb.Append(i == 0 ? "" : "&");
                sb.Append(Parameters[i].Name);
                sb.Append("=");
                var val = System.Net.WebUtility.UrlEncode(Parameters[i].Value.ToString());
                sb.Append(val);
            }

            HttpContent httpContent = new StringContent(sb.ToString());
            //Debug.WriteLine(content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


            HttpResponseMessage response = await client.PostAsync(_targetUrl+path, httpContent);              

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<System.Net.HttpStatusCode> SendPostToUriAsyncReturnHttpCode(string path)
        {
            var handler = new HttpClientHandler {UseDefaultCredentials = true, AllowAutoRedirect = false};

            var client = new HttpClient(handler);

            var sb = new StringBuilder();
            for (int i = 0; i < Parameters.Count; ++i)
            {
                if (Parameters[i] == null || Parameters[i].Name == null || Parameters[i].Value == null) continue;

                sb.Append(i == 0 ? "" : "&");
                sb.Append(Parameters[i].Name);
                sb.Append("=");
                sb.Append(Parameters[i].Value);
            }

            HttpContent httpContent = new StringContent(sb.ToString());
            //Debug.WriteLine(content);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


            HttpResponseMessage response = await client.PostAsync(_targetUrl + path, httpContent);

            return response.StatusCode;
        }
    }
}
