using System.Collections.Generic;

namespace ephoneTables
{
    class AddToSharepoint
    {
        public static void AddToSharepointTables()
        {
            FtpConnectFileGet getTheFile = new FtpConnectFileGet(GlobVar.ServerUri); //lista zawiera date modyfikacji modificationDate i nazwe pliku filename
            RouterConfig routerConfigObj = new RouterConfig();
            List<EphoneTuple> ephonePairedList;
            
            foreach (FtpFileModificationDate filenameAndModDateAndCme in getTheFile.GetUniqueRoutersList())
            {
                ephonePairedList = routerConfigObj.DownloadConfigurationFile(filenameAndModDateAndCme);
                RouterConfigToSharepoint configToSp = new RouterConfigToSharepoint(ephonePairedList, filenameAndModDateAndCme);
                //tutaj ma wywolywac klase dodajaca dane do sharepointa.
            }
        }
    }
}
