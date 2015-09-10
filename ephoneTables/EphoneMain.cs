using System;
using System.Collections.Generic;
using System.Linq;
using  System.Threading;

namespace ephoneTables
{
    public class EphoneMain
    {
        private Thread th = null;

        public EphoneMain()
        {
            if (th == null)
            {
                th = new Thread(new ThreadStart(MainLoop)) { Name = "Service Running Thread" };
                th.Start();
            }
        }

        public void MainLoop()
        {
            try
            {
                List<FtpFileModificationDate> lastModList = new List<FtpFileModificationDate>();
                int iteration = 0;

                while (true)
                {
                    Console.WriteLine("beggining {0} iteration...", iteration);
                    FtpConnectFileGet dates = new FtpConnectFileGet(GlobVar.ServerUri); //w srodku jest Filename, ModificationDate i RouterName dla wszystkich routerów
                    iteration++;
                    
                    if (lastModList.Count > 0)
                    {
                        if (lastModList.Count() < dates.Count)
                        {
                            DeleteOldSharepointRouterConfigTables.DeleteAll();
                            AddToSharepoint.AddToSharepointTables();
                            SendMail mail = new SendMail();
                            mail.SendEmail();
                        }
                        Thread.Sleep(600000);
                    }
                    else
                    {
                        lastModList = dates;
                    }
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }

        public void Abort()
        {
            th.Abort();
        }
    }
}
