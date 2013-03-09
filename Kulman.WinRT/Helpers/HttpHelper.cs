using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Kulman.WinRT.Helpers
{
    namespace HttpHelper
    {
        public sealed class Post
        {
            public IAsyncOperation<string> PostToUriAsync(string targetUri, string content)
            {
                return Task.Run(async () =>
                                          {
                                              var headers = await SendPostToUriAsync(targetUri, content);
                                              return headers;
                                          }).AsAsyncOperation();
            }

            private async Task<string> SendPostToUriAsync(string targetUri, string content)
            {
                var handler = new HttpClientHandler {UseDefaultCredentials = true, AllowAutoRedirect = false};

                var client = new HttpClient(handler);

                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.PostAsync(targetUri, httpContent);

                var headers = new Dictionary<string, string>();

                foreach (var header in response.Headers)
                {
                    string headerValue;

                    if (header.Value.Count() == 1)
                    {
                        headerValue = header.Value.First();
                    }
                    else
                    {
                        var stringValue = new StringBuilder();

                        foreach (var value in header.Value)
                        {
                            stringValue.Append(value);
                            stringValue.Append("\n");
                        }

                        headerValue = stringValue.ToString();
                    }

                    headers.Add(header.Key, headerValue);
                }

                //return headers;
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
    }
}