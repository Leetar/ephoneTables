using System.IO;
using System.Net;

namespace ephoneTables
{
    class ConnectToFtPgetReader
    {
        public StreamReader GetReaderMet(string serverUri)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential("crawl", "qwerty123");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                StreamReader reader = new StreamReader(responseStream);
                return reader;
            }
            else
            {
                return null;
            }
        }
    }
}
