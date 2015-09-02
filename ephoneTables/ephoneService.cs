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

    partial class ephoneService : ServiceBase
    {
        List<FTPFileModificationDate> lastModList = new List<FTPFileModificationDate>();
        public ephoneService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            bool i = true;
            while (i == true)
            {
                FTPConnectFileGet dates = new FTPConnectFileGet("ftp://172.17.56.20/CISCO");
                

                if (lastModList.Count > 0)
                {
                    for (int j = 0; j < lastModList.Count; j++)
                    {
                        if (lastModList[j].modificationDate != dates[j].modificationDate)
                        {

                        }
                    }
                }
                else
                {
                    lastModList = dates;
                }
                

            }


        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
