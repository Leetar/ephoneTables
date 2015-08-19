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
    public class FTPConnectFileGet : List<FTPFileModificationDate>
    {
        /// <summary>
        /// Konstruktor tworzący listę zwierającą nazwy plików i daty ich ostatniej modyfikacji
        /// </summary>
        /// <param name="serverUri">Adres IP routera</param>
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
                this.Add(new FTPFileModificationDate(serverUri, filename));
                
            }
        }

        public IEnumerable<FTPFileModificationDate> GetNewestRouter()
        {
            List<FTPFileModificationDate> resultList = new List<FTPFileModificationDate>();

            IEnumerable<string> routers = this.Select(x => x.routerName).Distinct(); //routerName jest w FTPFileModificationdate i tam trzeba napisać kod który go wycina. Lol.
            foreach (string unique_router_name in routers)
            {
                FTPFileModificationDate latestFile = this.Where(x => x.routerName == unique_router_name).OrderByDescending(x => x.modificationDate).First();
                latestFile.DownloadConfigurationFile(GlobVar.serverUri);
                resultList.Add(latestFile);
            }

            return resultList;
        }
    }
}
