using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    class RouterConfig
    {
        Dictionary<string, RouterSectionItems> ephone;
        Dictionary<string, RouterSectionItems> ephonedn;

        public void DownloadConfigurationFile(FTPFileModificationDate filename)
        {
            // pociagniecie configa
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("crawl", "qwerty123");
            StreamReader reader2 = new StreamReader(client.OpenRead(GlobVar.serverUri + filename.filename)); //tu bierze najnowszy plik
            string fileContent = reader2.ReadToEnd();

            //Match fileCME = Regex.Match(GlobVar.serverUri + lines[dateIndex], @"\w{14}");
            //GlobVar.fileCME = fileCME;


       
        }
    }
}
