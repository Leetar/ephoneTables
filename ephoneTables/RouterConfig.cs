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

            string currentSectionName = "";
            List<string> currentSectionLines = new List<string>();

            foreach (string configLine in fileContent.Split('\n'))
            {
                configLine.Trim();

                if(configLine == Regex.Match(configLine, @"\bephone\s+\d+\b").ToString())
                {
                    currentSectionName = configLine;
                }

                if (currentSectionName.Length > 0)
                {
                    // jezeli jest sekcja

                    if (configLine == "!")
                    {
                        ephone[currentSectionName] = 
                            new RouterSectionItems(currentSectionLines.ToArray());
                        currentSectionName = "";
                    }
                    else
                    {
                        currentSectionLines.Add(configLine);
                    }
                }
                else
                {
                    // jezeli nie ma sekcje

                    string[] groups = Regex.Split(configLine, @"\bephone\s+(\d+)\b");
                    // jezeli nie udalo sie znalesc sekcji
                    if (groups == null || groups.Length == 0)
                    {
                        continue;
                    }
                    currentSectionName = groups[0].Trim();
                }
            }

            ephone = new Dictionary<string, RouterSectionItems>();

            foreach (Match ephoneMatch in Regex.Matches(fileContent, @"\bephone\s+\d+\b"))
            {
                if(Regex.Match(fileContent, @"\bephone\s+\d+\b").Success)
                {
                    
                    Console.WriteLine(Regex.Match(fileContent, @"\bephone\s+\d+\b"));
                    //ephone.Add(Regex.Match(fileContent, @"\bephone\s+(\d+)\b").ToString(), 
                        //new RouterSectionItems(fileContent, filename, Regex.Match(fileContent, 
                       // @"\bephone\s+\d+\b").ToString()));
                }
            }

            //RouterSectionItems sectionItems = new RouterSectionItems(fileContent, filename);
            //Match fileCME = Regex.Match(GlobVar.serverUri + lines[dateIndex], @"\w{14}");
            //GlobVar.fileCME = fileCME;


       
        }
    }
}
//tylko w jednym pliku naraz