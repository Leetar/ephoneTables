using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;

namespace ephoneTables
{
    /// <summary>
    /// deletes all list items in a Sharepoint list
    /// </summary>
    class DeleteOldSharepointRouterConfigTables
    {
        /// <summary>
        /// deletes all list items in a Sharepoint list
        /// </summary>
        public static void deleteAll()
        {
            string sharepointURL = "http://sharepoint.eot.int/kb/";

            using (ClientContext ccontext = new ClientContext(sharepointURL))
            {
                Web web = ccontext.Web;
                List routerConfigListOnSharepoint = web.Lists.GetByTitle("Klienci VOIP");

                ListItemCollection listItems = routerConfigListOnSharepoint.GetItems(new CamlQuery());
                ccontext.Load(routerConfigListOnSharepoint);
                ccontext.Load(listItems);

                try
                {
                    ccontext.ExecuteQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + Environment.NewLine + "Terminating program due to error occurence...");
                    System.Threading.Thread.Sleep(5000);
                    System.Environment.Exit(0);
                }

                while (listItems.Count != 0)
                {
                    listItems[listItems.Count - 1].DeleteObject();
                    ccontext.ExecuteQuery();
                }
            }           
        }
    }
}
