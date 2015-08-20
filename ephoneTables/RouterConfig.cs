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

            string[] fileContentArray = fileContent.Split('\n');
            ephone = new Dictionary<string, RouterSectionItems>();

            foreach (Match ephoneMatch in Regex.Matches(fileContent, @"\bephone\s+\d+\b"))
            {
                if(Regex.Match(fileContent, @"\bephone\s+\d+\b").Success)
                {
                    Console.WriteLine(Regex.Match(fileContent, @"\bephone\s+\d+\b"));
                    ephone.Add(Regex.Match(fileContent, @"\bephone\s+(\d+)\b").ToString(), new RouterSectionItems(fileContentArray, filename, Regex.Match(fileContent, @"\bephone\s+\d+\b").ToString()));
                }
            }

            //RouterSectionItems sectionItems = new RouterSectionItems(fileContent, filename);
            //Match fileCME = Regex.Match(GlobVar.serverUri + lines[dateIndex], @"\w{14}");
            //GlobVar.fileCME = fileCME;


       
        }
    }
}
//tylko w jednym pliku naraz