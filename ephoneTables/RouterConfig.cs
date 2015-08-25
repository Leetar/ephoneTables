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
        Dictionary<string, RouterSectionItems> ephone = new Dictionary<string, RouterSectionItems>();
        Dictionary<string, RouterSectionItems> ephoneDN = new Dictionary<string, RouterSectionItems>();
        Dictionary<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>> pehonepairs;

        public void DownloadConfigurationFile(FTPFileModificationDate filename)
        {
            // pociagniecie configa
            string fileContent = GetConfigTextContentToString.ConfigContent(filename);
            DownloadSection(fileContent, @"\bephone\s+\d+.*\b", "ephone");
            DownloadSection(fileContent, @"\bephone-dn\s+\d+.*\b", "ephonedn");
            Console.WriteLine("dupa");
            
        }
        //=============================
        private Tuple<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>> DownloadSection(string fileContent, string expressionSectionType, string sectionName)
        {
            string currentSectionName = "";
            List<string> currentSectionLines = new List<string>();
            //Tuple<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>> ephonePairs;

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
                            throw new NotSupportedException();
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
            setEphonePairs();
            
        }
        //=============================
        private Tuple<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>> setEphonePairs()
        {
            Tuple<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>> ephonePairs = 
                new Tuple<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>>(ephone, ephoneDN);

            foreach(KeyValuePair<string, RouterSectionItems> pair in ephone)
            {
                string button;
                
            }

            return ephonePairs;
        }
    }
}
//tylko w jednym pliku naraz