using System.IO;
using System.Net;

namespace ephoneTables
{
    class GetConfigTextContentToString
    {
        public static string ConfigContent(FtpFileModificationDate filename)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("crawl", "qwerty123");
                StreamReader reader2 = new StreamReader(client.OpenRead(GlobVar.ServerUri + filename.Filename));

                return reader2.ReadToEnd();
            }
        }
    }
}
