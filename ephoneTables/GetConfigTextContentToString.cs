using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace ephoneTables
{
    class GetConfigTextContentToString
    {
        public static string ConfigContent(FTPFileModificationDate filename)
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("crawl", "qwerty123");
            StreamReader reader2 = new StreamReader(client.OpenRead(GlobVar.serverUri + filename.filename)); //tu bierze najnowszy plik
            return reader2.ReadToEnd();
        }
    }
}
