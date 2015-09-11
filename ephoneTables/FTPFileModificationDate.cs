using System;
using System.Net;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    public class FtpFileModificationDate// : List<FTPConnectFileGet>
    {
        public string Filename
        {
            get;
            private set;
        }
        public string RouterName
        {
            get;
            private set;
        }
        public DateTime ModificationDate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the date of when the file was last modified. Invoked in FTPConnectFileGet Class.
        /// </summary>
        /// <param name="serverUri">server IP adress</param>
        /// <param name="filename">name of the file currently processed</param>
        public FtpFileModificationDate(string serverUri, string filename)
        {
            Filename = filename;
            FtpWebRequest requestDate = (FtpWebRequest)WebRequest.Create(serverUri + Filename);
            requestDate.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            requestDate.Credentials = new NetworkCredential("crawl", "qwerty123");
            FtpWebResponse responseDate = (FtpWebResponse)requestDate.GetResponse();
                        
            ModificationDate = responseDate.LastModified;

            RouterName = Regex.Match(Filename, @"\b\w{3}_\d{3}_\d{2}_\w{3}\b").ToString();
        }

        public void DownloadConfigurationFile(string serverUri)
        {
            // pociagniecie configa
            FtpWebRequest requestDate = (FtpWebRequest)WebRequest.Create(serverUri + Filename);
            requestDate.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            requestDate.Credentials = new NetworkCredential("crawl", "qwerty123");
            //FtpWebResponse responseDate = (FtpWebResponse)requestDate.GetResponse();
        }
    }
}
