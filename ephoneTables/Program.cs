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
    class Program
    {
        static void Main(string[] args)
        {
            //czas wykonywania programu
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            GlobVar.serverUri = "ftp://172.17.56.20/CISCO/";

            DeleteOldSharepointRouterConfigTables.deleteAll();
            AddToSharepoint.AddToSharepointTables();
            SendMail mail = new SendMail();
            mail.SendEmail();

            

            


            //TestForMacDuplicates test = new TestForMacDuplicates();
            //test.getlistItems();

            AddToSharepoint.AddToSharepointTables();


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Operation complete. If log is empty, ephone records has not been found in newest file on server");
            Console.WriteLine("Execution time: " + ts);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


        }        
    }
}