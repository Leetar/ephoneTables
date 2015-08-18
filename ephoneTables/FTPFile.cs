using System;
using System.Net;

namespace ephoneTables
{
    public class FTPFile// : List<FTPConnectFileGet>
    {
        public string filename
        {
            get;
            private set;
        }
        public string router_name
        {
            get;
            private set;
        }
        public DateTime modificationDate
        {
            get;
            private set;
        }
        public string[] configRaw
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
            //router_name = filename.Remove()
        }

        public void DownloadConfiguration()
        {
            // pociagniecie configa
        }
    }
}
