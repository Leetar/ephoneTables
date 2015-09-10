using System.ServiceProcess;

namespace ephoneTables
{
    class Program
    {
        public static void Main()
        {
            //EphoneService service = new EphoneService();
            //ServiceInstallerUI installer = new ServiceInstallerUI(service.ServiceName);
            //installer.Start();
            GlobVar.ServerUri = "ftp://172.17.56.20/CISCO/";

#if (DEBUG)

            EphoneMain epmain = new EphoneMain();
            
#else
            
            ServiceBase[] servicesToRun = new ServiceBase[]
            {
                new EphoneService()
            };

            ServiceBase.Run(servicesToRun);
           
#endif
            
        }
    }
}