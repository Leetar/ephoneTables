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
            string fileContent = GetConfigTextContentToString.ConfigContent(filename);

            string[] fileContentArray = fileContent.Split('\n');
            ephone = new Dictionary<string, RouterSectionItems>();

            foreach (Match ephoneMatch in Regex.Matches(fileContent, @"\bephone\s+\d+\b"))
            {
                if(Regex.Match(fileContent, @"\bephone\s+\d+\b").Success)
                {
                    
                    Console.WriteLine(Regex.Match(fileContent, @"\bephone\s+\d+\b"));
                    ephone.Add(Regex.Match(fileContent, @"\bephone\s+(\d+)\b").ToString(), 
                        new RouterSectionItems(fileContent, filename, Regex.Match(fileContent, 
                        @"\bephone\s+\d+\b").ToString()));
                }
            }

            //RouterSectionItems sectionItems = new RouterSectionItems(fileContent, filename);
            //Match fileCME = Regex.Match(GlobVar.serverUri + lines[dateIndex], @"\w{14}");
            //GlobVar.fileCME = fileCME;


       
        }
    }
}
//tylko w jednym pliku naraz