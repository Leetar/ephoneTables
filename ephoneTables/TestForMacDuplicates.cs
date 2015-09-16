using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Client;

namespace ephoneTables
{
    class TestForMacDuplicates : List<MacAndCme>
    {
        public List<Tuple<MacAndCme, MacAndCme>> TestForMacDuplicatesMethod()
        {
            GetlistItems();
            //var duplicates = this.GroupBy(s => s.MAC);
            List<Tuple<MacAndCme, MacAndCme>> duplicates = new List<Tuple<MacAndCme, MacAndCme>>();

            foreach (MacAndCme element1 in this)
            {
                foreach (MacAndCme element2 in this)
                {
                    if (element1 == element2)
                    {
                        continue;
                    }

                    if (String.Equals(element1.Mac, element2.Mac, StringComparison.CurrentCultureIgnoreCase))
                    {

                        if (duplicates.Count > 0)
                        {
                            if (duplicates.Last().Item2.Ephone == element1.Ephone)
                                continue;
                        }


                        duplicates.Add(new Tuple<MacAndCme, MacAndCme>(element1, element2));
                    }
                }
            }
            return duplicates;
        }

        public void GetlistItems()
        {
            const string siteUrl = "http://sharepoint.eot.int/kb/";

            using (ClientContext clientContext = new ClientContext(siteUrl))
            {

                clientContext.Credentials = new NetworkCredential("automat.voip", "Hujkutas123", "EOT");
                List oList = clientContext.Web.Lists.GetByTitle("Klienci VOIP");

                CamlQuery camlQuery = new CamlQuery
                {
                    ViewXml =   "<View>"
                              + "<ViewFields><FieldRef Name='CME' /><FieldRef Name='MAC' /><FieldRef Name='EPHONE' /></ViewFields>"
                              + "</View>"
                };


                ListItemCollection collListItem = oList.GetItems(camlQuery);
                clientContext.Load(collListItem,
                    items => items.Include(
                    item => item["CME"],
                    item => item["MAC"],
                    item => item["EPHONE"]));

                clientContext.ExecuteQuery();

                foreach (ListItem oListItem in collListItem)
                {
                    this.Add(new MacAndCme()
                    {
                        Cme = oListItem["CME"].ToString(),
                        Mac = oListItem["MAC"].ToString(),
                        Ephone = oListItem["EPHONE"].ToString()
                    });
                }
            }
        }
    }
}
