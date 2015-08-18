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
    class ModificationDate : List<FTPConnectFileGet>
    {
        public List<DateTime> modificationDateList
        {
            get;
            private set;
        }
        public List<DateTime> dateModified(string serverUri, List<string> filesList) 
        {
            FTPConnectFileGet getFile = new FTPConnectFileGet();
            modificationDateList = new List<DateTime>();
            foreach (string line in filesList)
            {
                FtpWebRequest requestDate = (FtpWebRequest)WebRequest.Create(serverUri + line);
                requestDate.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                requestDate.Credentials = new NetworkCredential("crawl", "qwerty123");
                FtpWebResponse responseDate = (FtpWebResponse)requestDate.GetResponse();

                modificationDateList.Add(responseDate.LastModified);                
            }
            return modificationDateList;
        }
        public IEnumerable<FTPConnectFileGet> sort(List<DateTime> ModificationDateList)
        {
            return this.OrderBy(x => x.modificationDateList);
        }
    }
}
