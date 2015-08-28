using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;

namespace ephoneTables
{
    class DeleteOldSharepointRouterConfigTables
    {
        public static void deleteAll()
        {
            string sharepointURL = "http://sharepoint.eot.int/kb/";

            ClientContext ccontext = new ClientContext(sharepointURL);
            Web web = ccontext.Web;
            List routerConfigListOnSharepoint = web.Lists.GetByTitle("Klienci VOIP");
            
            ListItemCollection listItems = routerConfigListOnSharepoint.GetItems(new CamlQuery());
            ccontext.Load(routerConfigListOnSharepoint);
            ccontext.Load(listItems);
            ccontext.ExecuteQuery();
            
            while(listItems.Count != 0)
            {
                listItems[listItems.Count - 1].DeleteObject();
                ccontext.ExecuteQuery();
            }
            
        }
    }
}
