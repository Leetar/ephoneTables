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
            
            //Delete list entrires
            DeleteOldSharepointRouterConfigTables.deleteAll();


            foreach (FTPFileModificationDate filenameAndModDateAndCME in getTheFile.GetUniqueRoutersList())
            {
                //
                ephonePairedList = routerConfigObj.DownloadConfigurationFile(filenameAndModDateAndCME);
                routerConfigToSharepoint configToSP = new routerConfigToSharepoint(ephonePairedList, filenameAndModDateAndCME);
                //tutaj ma wywolywac klase dodajaca dane do sharepointa.
            }










            string spURL = "http://sp.eot.pl/Test";
            ClientContext clientcontext = new ClientContext(spURL);

            List oList = clientcontext.Web.Lists.GetByTitle("testEwidencja");
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem oListItem = oList.AddItem(itemCreateInfo);

            GroupCollection collGroup = clientcontext.Web.SiteGroups;
            Group oGroup = collGroup.GetById(6);
            UserCollection collUsr = oGroup.Users;


            clientcontext.Load(collUsr);

            /*int usrIndexSel = arrayValues(comboBox1.SelectedIndex); //get index of selected combobox item by arrayValues method.

            oListItem["Adresat"] = collUsr.GetById(usrIndexSel);
            oListItem["Title"] = tbNadawca.Text;
            oListItem["Opis"] = tbOpis.Text;
            oListItem["Lokalizacja"] = tbLokalizacja.Text;


            oListItem.Update();

            clientcontext.ExecuteQuery();
            labelProgress.Text = "Status: List Item creation complete.";*/
        }
    }
}
