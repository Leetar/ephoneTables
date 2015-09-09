using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace ephoneTables
{
    class AddToSharepoint
    {
        public static void AddToSharepointTables()
        {
            FTPConnectFileGet getTheFile = new FTPConnectFileGet(GlobVar.serverUri); //lista zawiera date modyfikacji modificationDate i nazwe pliku filename
            RouterConfig routerConfigObj = new RouterConfig();
            List<EphoneTuple> ephonePairedList = new List<EphoneTuple>();
            
            foreach (FTPFileModificationDate filenameAndModDateAndCME in getTheFile.GetUniqueRoutersList())
            {
                ephonePairedList = routerConfigObj.DownloadConfigurationFile(filenameAndModDateAndCME);
                routerConfigToSharepoint configToSP = new routerConfigToSharepoint(ephonePairedList, filenameAndModDateAndCME);
                //tutaj ma wywolywac klase dodajaca dane do sharepointa.
            }
        }
    }
}
