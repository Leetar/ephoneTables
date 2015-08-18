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
    public class FTPFile// : List<FTPConnectFileGet>
    {
        public string filename
        {
            get;
            private set;
        }
        public DateTime modificationDate
        {
            get;
            private set;
        }

        public FTPFile(string _serverUri, string _filename)
        {
            filename = _filename;
            FtpWebRequest requestDate = (FtpWebRequest)WebRequest.Create(_serverUri + filename);
            requestDate.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            requestDate.Credentials = new NetworkCredential("crawl", "qwerty123");
            FtpWebResponse responseDate = (FtpWebResponse)requestDate.GetResponse();
                        
            modificationDate = responseDate.LastModified;
        }
    }
}
