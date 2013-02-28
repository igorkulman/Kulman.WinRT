using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

// developed by Rico Suter (http://rsuter.com), http://mytoolkit.codeplex.com
// this code only works with mango (video urls with query don't work in previous versions)

namespace Kulman.WinRT.Multimedia
{
    public static class YouTube
    {
        public static Uri GetThumbnailUri(string youTubeId, YouTubeThumbnailSize size = YouTubeThumbnailSize.Medium)
        {
            switch (size)
            {
                case YouTubeThumbnailSize.Small:
                    return new Uri("http://img.youtube.com/vi/" + youTubeId + "/default.jpg", UriKind.Absolute);
                case YouTubeThumbnailSize.Medium:
                    return new Uri("http://img.youtube.com/vi/" + youTubeId + "/hqdefault.jpg", UriKind.Absolute);
                case YouTubeThumbnailSize.Large:
                    return new Uri("http://img.youtube.com/vi/" + youTubeId + "/maxresdefault.jpg", UriKind.Absolute);
            }
            throw new Exception();
        }

        public static async Task<string> GetUrl(string youTubeId, YouTubeQuality maxQuality = YouTubeQuality.Quality480P)
        {
            string youtubePage = await Kulman.WinRT.Helpers.WebHelper.DownloadPageAsync("http://www.youtube.com/watch?v=" + youTubeId);

            var urls = new List<YouTubeUrl>();
            try
            {
                var match = Regex.Match(youtubePage, "url_encoded_fmt_stream_map=(.*?)(&|\")");
                var data = Uri.UnescapeDataString(match.Groups[1].Value);

                var arr = data.Split(',');
                foreach (var d in arr)
                {
                    var tuple = new YouTubeUrl();
                    foreach (var p in d.Split('&'))
                    {
                        var index = p.IndexOf('=');
                        if (index != -1 && index < p.Length)
                        {
                            try
                            {
                                var key = p.Substring(0, index);
                                var value = Uri.UnescapeDataString(p.Substring(index + 1));
                                if (key == "url")
                                    tuple.Url = value;
                                else if (key == "itag")
                                    tuple.Itag = int.Parse(value);
                                else if (key == "type" && value.Contains("video/mp4"))
                                    tuple.Type = value;
                            }
                            catch { }
                        }
                    }

                    if (tuple.IsValid)
                        urls.Add(tuple);
                }

                var itag = GetQualityIdentifier(maxQuality);
                foreach (var u in urls.Where(u => u.Itag > itag).ToArray())
                    urls.Remove(u);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Getting video failed with " + ex.ToString());
                return string.Empty;
            }

            var entry = urls.OrderByDescending(u => u.Itag).FirstOrDefault();
            if (entry != null)
            {


                var url = entry.Url;
                return url;
            }

            return string.Empty;

            //return Http.Get("http://www.youtube.com/watch?v=" + youTubeId, r => OnHtmlDownloaded(r, maxQuality, onFinished));
        }

     
        
        private static int GetQualityIdentifier(YouTubeQuality quality)
        {
            switch (quality)
            {
                case YouTubeQuality.Quality480P: return 18;
                case YouTubeQuality.Quality720P: return 22;
                case YouTubeQuality.Quality1080P: return 37;
            }
            throw new ArgumentException("maxQuality");
        }

        

        private class YouTubeUrl
        {
            public string Url { get; set; }
            public int Itag { get; set; }
            public string Type { get; set; }

            public bool IsValid
            {
                get { return Url != null && Itag > 0 && Type != null; }
            }
        }
        
        #region Phone


        #endregion
    }
}
