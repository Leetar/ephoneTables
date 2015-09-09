using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ephoneTables
{

    partial class EphoneService : ServiceBase
    {
        List<FtpFileModificationDate> _lastModList = new List<FtpFileModificationDate>(); // na początku będzie puste
        public EphoneService()
        {
            InitializeComponent();

            this.ServiceName = "Ephone Configs To Sharepoint";
            this.EventLog.Log = "Application";

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            const bool i = true;
            int iteration = 0;
            while (i == true)
            {
                Console.WriteLine("beggining {0} iteration...", iteration);
                FtpConnectFileGet dates = new FtpConnectFileGet("ftp://172.17.56.20/CISCO"); //w srodku jest Filename, ModificationDate i RouterName dla wszystkich routerów
                iteration++;

                if (_lastModList.Count > 0)
                {
                    if (_lastModList.Count() < dates.Count)
                    {
                        Program.Main();
                    }
                    System.Threading.Thread.Sleep(600000);
                }
                else
                {
                    _lastModList = dates;
                    Program.Main();
                }
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
