using System;
using System.Net;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    public class FTPFileModificationDate// : List<FTPConnectFileGet>
    {
        public string filename
        {
            get;
            private set;
        }
        public string routerName
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
        /// <summary>
        /// Gets the date of when the file was last modified. Invoked in FTPConnectFileGet Class.
        /// </summary>
        /// <param name="_serverUri">server IP adress</param>
        /// <param name="_filename">name of the file currently processed</param>
        public FTPFileModificationDate(string _serverUri, string _filename)
        {
            filename = _filename;
            FtpWebRequest requestDate = (FtpWebRequest)WebRequest.Create(_serverUri + filename);
            requestDate.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            requestDate.Credentials = new NetworkCredential("crawl", "qwerty123");
            FtpWebResponse responseDate = (FtpWebResponse)requestDate.GetResponse();
                        
            modificationDate = responseDate.LastModified;

            routerName = Regex.Match(filename, @"\b\w{3}_\d{3}_\d{2}_\w{3}\b").ToString();
        }

        public void DownloadConfigurationFile(string _serverUri)
        {
            // pociagniecie configa
            FtpWebRequest requestDate = (FtpWebRequest)WebRequest.Create(_serverUri + filename);
            requestDate.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            requestDate.Credentials = new NetworkCredential("crawl", "qwerty123");
            FtpWebResponse responseDate = (FtpWebResponse)requestDate.GetResponse();
        }
    }
}
