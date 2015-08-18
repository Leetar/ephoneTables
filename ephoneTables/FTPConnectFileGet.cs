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
    public class FTPConnectFileGet : List<FTPFile>
    {
        
        public FTPConnectFileGet(string serverUri)
        {
            ConnectToFTPgetReader getReader = new ConnectToFTPgetReader();
            string[] lines = getReader.getReaderMet(serverUri).ReadToEnd().Split('\n');
            
            foreach (string line in lines)
            {
                if (line == "")
                {
                    break;
                }

                string filename = Regex.Replace(line, @"\t|\n|\r", "");
                this.Add(new FTPFile(serverUri, filename));
                
            }
        }

        public IEnumerable<FTPFile> get_fresh_files()
        {
            List<FTPFile> resultList = new List<FTPFile>();

            IEnumerable<string> routers = this.Select(x => x.router_name).Distinct();
            foreach (string unique_router_name in routers)
            {
                FTPFile latestFile = this.Where(x => x.router_name == unique_router_name).OrderByDescending(x => x.modificationDate).First();
                latestFile.DownloadConfiguration();
                resultList.Add(latestFile);
            }

            return resultList;
        }
    }
}
