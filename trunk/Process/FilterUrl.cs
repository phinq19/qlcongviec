using System;
using System.Collections.Generic;
using System.Text;

namespace PostForum.Modules
{
    public class FilterUrl
    {
        public static string Src
        {
            get { return "[sS][rR][cC][\\s]*=[\"'\\s]*([^\"'#\\s>]+)[\"']*"; }
        }

        public static string Background
        {
            get { return "[bB][aA][cC][]kK][gG][rR][oO][uU][nN][dD][\\s]*=[\"'\\s]*([^\"'#\\s>]+)[\"']*"; }
        }

        public static string Href
        {
            get { return "[hH][rR][eE][fF][\\s]*=[\"'\\s]*([^\"'#\\s>]+)[\"']*"; }
        }

        public static string UrlNotParam(string url)
        {
            if (url.LastIndexOf("?") != -1)
                return url.Substring(0, url.LastIndexOf("?"));
            return url;
        }

        public static string AbstractUrl(string url)
        {
            if (url.LastIndexOf("/") != -1)
                return url.Substring(0, url.LastIndexOf("/"));
            return url;
        }

        public static string RootLink(string url)
        {
            return Filter.GetTextByRegex(@"http://[\w\.\-_]+/", url);
        }

        /// <summary>
        /// Hàm trả về link tuyệt đối
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="link">href hoặc src</param>
        /// <returns></returns>
        public static string AbsoluteUrl(string url, string link)
        {
            if (link.IndexOf("http://") != -1)
                return link;

            if (link.IndexOf("/") == 0)
                return RootLink(url.TrimEnd('/')) + link;

            return AbstractUrl(url) + "/" + link;
        }
    }
}
