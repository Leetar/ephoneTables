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
    class Program
    {
        static void Main(string[] args)
        {
            

            //czas wykonywania programu
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            GlobVar.serverUri = "ftp://172.17.56.20/CISCO/";

            FTPConnectFileGet getTheFile = new FTPConnectFileGet(GlobVar.serverUri); //lista zawiera date modyfikacji modificationDate i nazwe pliku filename
            RouterConfig routerConfigObj = new RouterConfig();


            Console.WriteLine(getTheFile.Max(x => x.modificationDate));

            foreach (FTPFileModificationDate file in getTheFile.GetNewestRouter())
            {

                routerConfigObj.DownloadConfigurationFile(file);
            }

            //CiscoConfigSection:
            //Dictionary<string, string>

            Dictionary<string, Dictionary<string, string>> ephone;
            Dictionary<string, Dictionary<string, string>> ephonedn;

            /*foreach (string ephone_number in ephone.Keys)
            {
                if (!ephone[ephone_number].ContainsKey("name"))
                {

                }


                try
                {
                    ephone[ephone_number]["name"];
                }
                catch (Exception ex)
                {

                }

            }*/


            //Console.WriteLine(latestFile);





            /*//List<string> filesList = new List<string>(getTheFile.FTPConnect());

            //FTPFile modDate = new FTPFile();
            //List<DateTime> listOfDates = new List<DateTime>(modDate.dateModified("ftp://172.17.56.20/CISCO/", filesList));



            //int j = 0;
            //List<DateTime> listDateTime = new List<DateTime>();                    



            //int dateIndex = 0;
            //dateIndex = listOfDates.IndexOf(listDateTime.Max());

            //bierze tresc pliku do streamreadera a potem przenosi do zmiennej typu string
            /*WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("crawl", "qwerty123");
            //StreamReader reader2 = new StreamReader(client.OpenRead(serverUri + lines[dateIndex])); //tu bierze najnowszy plik
            StreamReader reader2 = new StreamReader(client.OpenRead(serverUri + "RTR_100_00_GNS-configAug--1-23-57-06.184-7")); //hardcode konkretnego pliku w celach debugu

            Match fileCME = Regex.Match(serverUri + lines[dateIndex], @"\w{14}");
            GlobVar.fileCME = fileCME;

            string fileContent = reader2.ReadToEnd();

            for (int i = 0; i < lines.Count() - 1; i++) // DRUKUJE LISTĘ DAT
            {
                //Console.WriteLine(listDateTime[i].ToString());
            }

            searchThroughFile(fileContent);*/

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Operation complete. If log is empty, ephone records has not been found in newest file on server");
            Console.WriteLine("Execution time: " + ts);
            Console.ReadKey();


        }
        /// <summary>
        /// Metoda wyszukujaca dane w tresci plikow
        /// </summary>
        /// <param name="fileContent">string zawierajacy cala zawartosc pliku</param>
        static void searchThroughFile(string fileContent) //OPERACJE NA TRESCI NAJNOWSZEGO PLIKU
        {
            Regex searchForKeyword = new Regex(@"\bempty");

            List<string[]> fileContentList = new List<string[]>();
            string[] fileContentArr = fileContent.Split('\n');

            Match ephoneNameMatch;
            Match typeMatch;
            Match primaryDNMatch; //numer z nazwy 'ephone-dn xxx'
            Match primaryDNnumberMatch;
            Match primaryDNLabelMatch;

            //test
            string s = "empty";
            Match emptyMatch = Regex.Match(s, @"\b(empty)\b");

            int temp = 0, iter1 = 0;

            for (int i = 0; i < fileContentArr.Count(); i++)
            {
                foreach (Match m in Regex.Matches(fileContentArr[i], @"\bephone\s+.*"))
                {
                    temp++;
                }
            }
            GlobVar.daneDoTabelki = new Match[temp, 7]; //0 - ephone name; 1 - mac; 2 - type; 3 - button; 4 - numer z nazwy ephone-dn; 5 - number; 6 - label
                                                        //temp = 0;

            for (int i = 0; i < fileContentArr.Count(); i++)
            {
                foreach (Match m in Regex.Matches(fileContentArr[i], @"^\bephone\s+.*"))
                {
                    ephoneNameMatch = Regex.Match(fileContentArr[i], @"\b\d{1,4}");
                    GlobVar.daneDoTabelki[iter1, 0] = ephoneNameMatch;
                    
                    //tutaj na ponizsza linie przerywac jesli jest "!"
                    for (int ii = i; ii < 20; ii++)
                    {
                        Console.WriteLine("Match found" + Environment.NewLine);
                        if (fileContentArr[iter1] != "!")
                        {
                            //Console.WriteLine(fileContentArr[ii]);
                        }
                    }

                    for (int j = i; j < i + 20; j++) //szuka matchów dla tabeli - powinno działać do "!"
                    {
                        if (fileContentArr[j] == "!")
                        {
                            break;
                        }
                        foreach (Match n in Regex.Matches(fileContentArr[j], @"\bmac\-address.*")) //ZAPISANIE ADRESU MAC
                        {
                            GlobVar.macMatch = Regex.Match(fileContentArr[j], @"\b\w{4}\.\w{4}\.\w{4}");
                            GlobVar.daneDoTabelki[iter1, 1] = GlobVar.macMatch;
                        }
                        foreach (Match n in Regex.Matches(fileContentArr[j], @"\btype.*")) //ZAPISANIE TYPU
                        {
                            if (Regex.Match(fileContentArr[j], @"\b\d{4}").Success) //Jeśli numer
                            {
                                typeMatch = Regex.Match(fileContentArr[j], @"\b\d{4}\b");
                                GlobVar.daneDoTabelki[iter1, 2] = typeMatch;
                            }
                            else if (Regex.Match(fileContentArr[j], @"\b\w{3}\b").Success) //Jeśli tekst
                            {
                                typeMatch = Regex.Match(fileContentArr[j], @"\b\w{3}\b");
                                GlobVar.daneDoTabelki[iter1, 2] = typeMatch;
                            }
                        }
                        foreach (Match n in Regex.Matches(fileContentArr[j], @"\bbutton\s+1:\d{1,4}")) //ZAPISANIE BUTTONA. 
                        {
                            GlobVar.buttonMatchGlob = Regex.Match(fileContentArr[j], @"button\s+1:(\d{1,4})");
                            GlobVar.daneDoTabelki[iter1, 3] = GlobVar.buttonMatchGlob;

                            for (int itDN = 0; itDN < fileContentArr.Count(); itDN++)
                            {
                                foreach (Match p in Regex.Matches(fileContentArr[itDN], @"\bephone-dn\s+" + Regex.Escape(GlobVar.buttonMatchGlob.Groups[1].ToString()) + @"\b")) // TU JEST JAKIS POWAŻNY BŁĄD!!! 
                                {
                                    primaryDNMatch = Regex.Match(fileContentArr[itDN], @"\bephone-dn\s+(\d{1,4})");
                                    GlobVar.daneDoTabelki[iter1, 4] = primaryDNMatch;

                                    for (int jj = itDN; jj < itDN + 20; jj++) //szuka matchów dla tabeli - powinno działać do "!"
                                    {
                                        if (fileContentArr[jj] == "!")
                                        {
                                            break;
                                        }
                                        foreach (Match o in Regex.Matches(fileContentArr[jj], @"\bnumber\s+\d{1,4}")) //NUMER Z EPHONE-DN. NIE ZROBIONE JESZCZE!
                                        {
                                            primaryDNnumberMatch = Regex.Match(fileContentArr[jj], @"\b\d{4}\b");
                                            GlobVar.daneDoTabelki[iter1, 5] = primaryDNnumberMatch;
                                        }
                                        foreach (Match o in Regex.Matches(fileContentArr[jj], @"\blabel\s+.+")) //NUMER Z EPHONE-DN. NIE ZROBIONE JESZCZE!
                                        {
                                            primaryDNLabelMatch = Regex.Match(fileContentArr[jj], @"\blabel\s+(.+)");
                                            GlobVar.daneDoTabelki[iter1, 6] = primaryDNLabelMatch;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    iter1++;
                }
            }

            for (int i = 0; i < temp; i++) //drukowanie zawaartosci tabelki do konsoli
            {
                for (int ii = 0; ii < 7; ii++)
                {
                    /*if (Regex.Match(daneDoTabelki[i, ii].ToString(), @"\bbutton\s+1:\d{1,4}").Success) //to ma robic nowa linie gdy skonczyl sie ciag do jedego ephonea
                    {
                        Console.WriteLine(Environment.NewLine);
                    }*/
                    if (GlobVar.daneDoTabelki[i, ii] == null)
                    {
                        Console.WriteLine("null");
                    }
                    else
                    {
                        Console.WriteLine(GlobVar.daneDoTabelki[i, ii].ToString());

                    }

                }
                Console.WriteLine(Environment.NewLine);
            }

        }
    }

}