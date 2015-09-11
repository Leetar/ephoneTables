using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    class RouterConfig
    {
        Dictionary<string, RouterSectionItems> _ephone = new Dictionary<string, RouterSectionItems>();
        Dictionary<string, RouterSectionItems> _ephoneDn = new Dictionary<string, RouterSectionItems>();
        //Dictionary<Dictionary<string, RouterSectionItems>, Dictionary<string, RouterSectionItems>> ehonepairs;

        public List<EphoneTuple> DownloadConfigurationFile(FtpFileModificationDate filename)
        {
            // pociagniecie configa
            string fileContent = GetConfigTextContentToString.ConfigContent(filename);
            _ephone.Clear();
            _ephoneDn.Clear();
            DownloadSection(fileContent, @"\bephone\s+\d+.*\b", "ephone");
            DownloadSection(fileContent, @"\bephone-dn\s+\d+.*\b", "ephonedn");
            List<EphoneTuple> ephonePairedList = new List<EphoneTuple>();

            ephonePairedList = SetEphonePairs();
            //Console.WriteLine(ephonePairedList);
            return ephonePairedList;

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
                            _ephone[currentSectionName] =
                           new RouterSectionItems(currentSectionLines.ToArray());
                        }
                        else if (sectionName == "ephonedn")
                        {
                            _ephoneDn[currentSectionName] =
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
            

        }
        //=============================
        private List<EphoneTuple> SetEphonePairs()
        {
            EphoneTuple ephonePairs;
            List<EphoneTuple> ephonePairList = new List<EphoneTuple>();

            foreach(var ephoneEntry in _ephone)
            {
                var ephoneEntryValue = ephoneEntry.Value;
                if (ephoneEntryValue.Keys.Contains("button"))
                {
                    foreach (var ephoneDnEntry in _ephoneDn)
                    {
                        Match ephoneDnNumberMatch = Regex.Match(ephoneDnEntry.Key, @"\bephone-dn\s+(\d+).*\b");
                        string ephoneDNnumber = ephoneDnNumberMatch.Groups[1].ToString();
                        string[] buttonNumberNormal = ephoneEntryValue["button"].Split(':', ' ');

                        if (buttonNumberNormal[1] == ephoneDNnumber)
                        {
                            ephonePairs = new EphoneTuple(ephoneEntry, ephoneDnEntry);
                            ephonePairList.Add(ephonePairs); // prawdopodobnie skonczone. Dodawanie par do listy.
                        }
                    }
                }
            }
            return ephonePairList;
        }
    }
}
//tylko w jednym pliku naraz