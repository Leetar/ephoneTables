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
    public class FTPConnectFileGet
    {
        public FtpWebResponse response
        {
            get;
            set;
        }
        public FtpWebRequest request
        {
            get;
            set;
        }
        public List<string> fileLinesList
        {
            get;
            private set;
        }
        
        public List<string> FTPConnect(string serverUri)
        {
            ConnectToFTPgetReader getReader = new ConnectToFTPgetReader();
            string[] lines = getReader.getReaderMet(serverUri).ReadToEnd().Split('\n');
            
            fileLinesList = new List<string>();

            foreach (string line in lines)
            {
                if (line == "") { break; }
                fileLinesList.Add(Regex.Replace(line, @"\t|\n|\r", ""));
                
            }
            return fileLinesList;
            //ModificationDate modDate = new ModificationDate();
            //modDate.dateModified(serverUri);
            
            
        }
    }
}
