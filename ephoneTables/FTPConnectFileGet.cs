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
    }
}
