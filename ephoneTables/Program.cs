using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ephoneTables
{
    class Program
    {
        public static void Main()
        {
            //progam execution time
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            GlobVar.ServerUri = "ftp://172.17.56.20/CISCO/";

            DeleteOldSharepointRouterConfigTables.DeleteAll();

            AddToSharepoint.AddToSharepointTables();

            SendMail mail = new SendMail();
            mail.SendEmail();
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Iteration successful.");
            Console.WriteLine("Iteration execution time: {0}", ts);


        }        
    }
}