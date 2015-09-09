using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.SharePoint.Client;

namespace ephoneTables
{
    class TestForMacDuplicates : List<MacAndCME>
    {
        public List<Tuple<MacAndCME, MacAndCME>> TestForMacDuplicatesMethod()
        {
            getlistItems();
            //var duplicates = this.GroupBy(s => s.MAC);
            List<Tuple<MacAndCME, MacAndCME>> duplicates = new List<Tuple<MacAndCME, MacAndCME>>();

            foreach (MacAndCME element1 in this)
            {
                foreach (MacAndCME element2 in this)
                {
                    if (element1 == element2)
                    {
                        continue;
                    }

                    if (element1.MAC.ToUpper() == element2.MAC.ToUpper())
                    {

                        if (duplicates.Count > 0)
                        {
                            if (duplicates.Last().Item2.EPHONE == element1.EPHONE)
                                continue;
                        }


                        duplicates.Add(new Tuple<MacAndCME, MacAndCME>(element1, element2));
                    }
                }
            }
            return duplicates;
        }

        public void getlistItems()
        {
            string siteUrl = "http://sharepoint.eot.int/kb/";

            using (ClientContext clientContext = new ClientContext(siteUrl))
            {


                List oList = clientContext.Web.Lists.GetByTitle("Klienci VOIP");

                CamlQuery camlQuery = new CamlQuery();

                camlQuery.ViewXml =
                      "<View>"
                    + "<ViewFields><FieldRef Name='CME' /><FieldRef Name='MAC' /><FieldRef Name='EPHONE' /></ViewFields>"
                    + "</View>";

                ListItemCollection collListItem = oList.GetItems(camlQuery);
                clientContext.Load(collListItem,
                    items => items.Include(
                    item => item["CME"],
                    item => item["MAC"],
                    item => item["EPHONE"]));

                clientContext.ExecuteQuery();

                foreach (ListItem oListItem in collListItem)
                {
                    this.Add(new MacAndCME()
                    {
                        CME = oListItem["CME"].ToString(),
                        MAC = oListItem["MAC"].ToString(),
                        EPHONE = oListItem["EPHONE"].ToString()
                    });
                }
            }
        }
    }
}
