using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Xml;

namespace ephoneTables
{
    class routerConfigToSharepoint
    {
        public routerConfigToSharepoint(List<EphoneTuple> ephonePairedList, FTPFileModificationDate filenameAndModDateAndCME)
        {
            string sharepointURL = "http://sharepoint.eot.int/kb/";

            ClientContext ccontext = new ClientContext(sharepointURL);
            Web web = ccontext.Web;

            List itemList = web.Lists.GetByTitle("Klienci VOIP");

            ccontext.Load(itemList.Fields);
            ccontext.ExecuteQuery();

            XmlDocument XmlCitiesTranslationList = new XmlDocument();
            try
            {
                XmlCitiesTranslationList.Load("CityCodeTranslation.xml");
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //XmlCitiesTranslationList


            foreach (Field field in itemList.Fields)
            {
                Console.WriteLine(field.Title + " " + field.InternalName);
            }

            ListItemCreationInformation newItem = new ListItemCreationInformation();

            string[] splittedButton;
            string[] splittedDNnumber;

            //string itemInternalName = itemList.Fields.GetByInternalNameOrTitle("PRIMARY DN").InternalName;

            foreach (EphoneTuple element in ephonePairedList)
            {
                ListItem listItem = itemList.AddItem(newItem);
                listItem["CME"] = filenameAndModDateAndCME.routerName;
                listItem["EPHONE"] = element.Item1.Value["ephone"];

                if (element.Item1.Value.ContainsKey("mac-address"))
                {
                    listItem["MAC"] = element.Item1.Value["mac-address"];
                }
                else
                {
                    listItem["MAC"] = "BRAK!";
                }
                if (element.Item1.Value.ContainsKey("type"))
                {
                    listItem["TYPE"] = element.Item1.Value["type"];
                }
                else
                {
                    listItem["TYPE"] = "BRAK!";
                }
                if (element.Item1.Value.ContainsKey("button"))
                {
                    splittedButton = element.Item1.Value["button"].Split(':', ' ');
                    listItem["PRIMARY_x0020_DN"] = splittedButton[1];
                }
                else
                {
                    listItem["PRIMARY_x0020_DN"] = "BRAK!";
                }
                if (element.Item2.Value.ContainsKey("number"))
                {
                    splittedDNnumber = element.Item2.Value["number"].Split(' ');
                    listItem["Title"] = splittedDNnumber[0]; // PRIMARY DN NUMBER
                }
                else
                {
                    listItem["Title"] = "BRAK!"; //PRIMARY DN NUMBER
                }
                if (element.Item2.Value.ContainsKey("label"))
                {
                    listItem["PRIMARY_x0020_DN_x0020_NUMBER"] = element.Item2.Value["label"]; // PRIMARY DN LABEL
                }
                else
                {
                    listItem["PRIMARY_x0020_DN_x0020_NUMBER"] = "BRAK!"; // PRIMARY DN LABEL
                }
                listItem.Update();
                
            }
            ccontext.ExecuteQuery();
            Console.WriteLine("DURP");

        }
    }
}
