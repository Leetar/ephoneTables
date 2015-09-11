using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    public class FtpConnectFileGet : List<FtpFileModificationDate>
    {
        /// <summary>
        /// Konstruktor tworzący listę zwierającą nazwy plików i daty ich ostatniej modyfikacji
        /// </summary>
        /// <param name="serverUri">Adres IP routera</param>
        public FtpConnectFileGet(string serverUri)
        {
            ConnectToFtPgetReader getReader = new ConnectToFtPgetReader();
            
            string[] lines = getReader.GetReaderMet(serverUri).ReadToEnd().Split('\n');

            if (lines == null)
            {
                EventLogging.LogEvent("Reader has returned null", true);
            }

            foreach (string line in lines)
            {
                if (line == "")
                {
                    break;
                }

                string filename = Regex.Replace(line, @"\t|\n|\r", "");
                this.Add(new FtpFileModificationDate(serverUri, filename));
            }
        }

        public IEnumerable<FtpFileModificationDate> GetUniqueRoutersList()
        {
            List<FtpFileModificationDate> resultList = new List<FtpFileModificationDate>();

            IEnumerable<string> routers = this.Select(x => x.RouterName).Distinct(); //routerName jest w FTPFileModificationdate i tam trzeba napisać kod który go wycina. Lol.
            foreach (string uniqueRouterName in routers)
            {
                FtpFileModificationDate latestFile = this.Where(x => x.RouterName == uniqueRouterName).OrderByDescending(x => x.ModificationDate).First();
                latestFile.DownloadConfigurationFile(GlobVar.ServerUri);
                resultList.Add(latestFile);
            }

            return resultList;
        }
    }
}
