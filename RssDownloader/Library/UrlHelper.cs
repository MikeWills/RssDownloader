using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDownloader.Library
{
    class UrlHelper
    {
        public static string GetFileName(string url)
        {
            Uri uri = new Uri(url);
            return System.IO.Path.GetFileName(uri.AbsolutePath);
        }
    }
}
