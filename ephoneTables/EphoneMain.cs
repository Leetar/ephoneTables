using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ephoneTables
{
    public class EphoneMain
    {
        private Thread th = null;

        public EphoneMain()
        {
            th = new Thread(new ThreadStart(MainLoop)) { Name = "Service Running Thread" };
            th.Start();
        }

        public void MainLoop()
        {
            try
            {
                List<FtpFileModificationDate> lastModList = new List<FtpFileModificationDate>();
                int iteration = 0;

                while (true)
                {
                    if (iteration == 0)
                    {
                        DeleteOldSharepointRouterConfigTables.DeleteAll();
                        AddToSharepoint.AddToSharepointTables();
                        SendMail mail = new SendMail();
                        mail.SendEmail();
                        EventLogging.LogEvent(
                            "Deletion and reprint of router config information has been performed. Iteration " +
                            iteration, false);

                        Console.WriteLine(
                            "Deletion and reprint of router config information has been performed. Iteration " +
                            iteration);
                    }


                    EventLogging.LogEvent("Iteration " + iteration + " complete", false);

                    Console.WriteLine("beggining {0} iteration... " + DateTime.Now, iteration);
                    FtpConnectFileGet dates = new FtpConnectFileGet(GlobVar.ServerUri);
                        //w srodku jest Filename, ModificationDate i RouterName dla wszystkich routerów
                    iteration++;
                    
                    if (lastModList.Count > 0)
                    {
                        if (lastModList.Count() != dates.Count)
                        {
                            DeleteOldSharepointRouterConfigTables.DeleteAll();
                            AddToSharepoint.AddToSharepointTables();
                            SendMail mail = new SendMail();
                            mail.SendEmail();
                            EventLogging.LogEvent(
                                "Deletion and reprint of router config information has been performed. Iteration " +
                                iteration, false);

                            Console.WriteLine(
                                "Deletion and reprint of router config information has been performed. Iteration " +
                                iteration);
                        }
                        Thread.Sleep(300000);
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
            catch (Exception ex)
            {
                EventLogging.LogEvent(ex.ToString(), true);
            }
        }

        public void Abort()
        {
            th.Abort();
        }
    }
}
