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
        public Dictionary<string, RouterSectionItems> ephone = new Dictionary<string, RouterSectionItems>();
        Dictionary<string, RouterSectionItems> ephoneDN = new Dictionary<string, RouterSectionItems>();

        public void DownloadConfigurationFile(FTPFileModificationDate filename)
        {
            // pociagniecie configa
            string fileContent = GetConfigTextContentToString.ConfigContent(filename);
            DownloadSection(fileContent, @"\bephone\s+\d+.*\b", "ephone");
            DownloadSection(fileContent, @"\bephone-dn\s+\d+.*\b", "ephonedn");
            Console.WriteLine("dupa");
            
        }
        //=============================
        private void DownloadSection(string fileContent, string expressionSectionType, string sectionName)
        {
            string currentSectionName = "";
            List<string> currentSectionLines = new List<string>();
            

            foreach (string configLine in fileContent.Split('\n'))
            {
                configLine.Trim();

                if (configLine == Regex.Match(configLine, expressionSectionType).ToString())
                {
                    currentSectionName = configLine;

                }                

                if (currentSectionName.Length > 0)
                {
                    // jezeli jest sekcja

                    if (configLine == "!")
                    {
                        if (sectionName == "ephone")
                        {
                            ephone[currentSectionName] =
                           new RouterSectionItems(currentSectionLines.ToArray());
                        }
                        else if (sectionName == "ephonedn")
                        {
                            ephoneDN[currentSectionName] =
                           new RouterSectionItems(currentSectionLines.ToArray());
                        }
                        else
                        {

                        }
                                               
                        currentSectionName = "";
                        currentSectionLines.Clear();
                    }
                    else
                    {
                        currentSectionLines.Add(configLine);
                    }
                }
            }
        }       
    }
}
//tylko w jednym pliku naraz