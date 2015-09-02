using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ephoneTables
{
    class TestForMacDuplicates
    {
        private TestForMacDuplicates(List<EphoneTuple> pairedSections)
        {
            for (int i = 0; i < pairedSections.Count; i++)
            {
                for (int j = 0; j < pairedSections.Count; j++)
                {
                    if (pairedSections[i].Item1.Value.ContainsKey("mac-address") == 
                        pairedSections[j].Item1.Value.ContainsKey("mac-address"))
                    {
                        MailMessage message = new MailMessage("sharepoint@eot.pl", "it.eot.pl"); // TEMPORARY
                        SmtpClient mailClient = new SmtpClient();
                        mailClient.Port = 25;
                        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mailClient.UseDefaultCredentials = false;
                        mailClient.Host = "smtp.google.com"; //TEMPORARY
                        message.Subject = "FOUND MAC MDDRESS DUPLICATE";
                        message.Body = "mac adress for " +
                            pairedSections[i].Item1.Key +
                            " and " + pairedSections[j].Item1.Key + " the same!";
                        mailClient.Send(message);

                        break;
                    }
                }
            }
        }
    }
}
