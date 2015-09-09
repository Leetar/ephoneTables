using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Xml;
using System.Web;

namespace ephoneTables
{
    class SendMail : List<string>
    {
        /// <summary>
        /// sends email after extracting email addresses from xml file (MailList.xml) and formatting the body of the message
        /// </summary>
        public void SendEmail()
        {
            try
            {
                TestForMacDuplicates dup = new TestForMacDuplicates();
                List<Tuple<MacAndCME, MacAndCME>> duplicates = dup.TestForMacDuplicatesMethod();

                if (duplicates.Count != 0)
                {
                    using (MailMessage message = new MailMessage())
                    {
                        using (SmtpClient smtpServer = new SmtpClient("mail.eot.pl"))
                        {
                            message.From = new MailAddress("voip@eot.pl");
                            ExtractMailAddressesFromXml(); // adds email addresses to 'this' from xml file

                            foreach (string recipient in this)
                            {
                                message.To.Add(recipient);
                            }
                            this.Clear();

                            message.Subject = "EPHONE MAC DUPLICATES REPORT";
                            message.Body = formatMailBody(duplicates).ToString();

                            message.IsBodyHtml = true;

                            smtpServer.Port = 25;

                            smtpServer.Send(message);
                        }
                    }
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Extracts recipients email addressed from XML file (MailList.xml) and puts them in a class (as it inherits from a List) 
        /// </summary>
        private void ExtractMailAddressesFromXml()
        {
            XmlDocument mailList = new XmlDocument();

            try
            {
                mailList.Load("MailList.xml");

                for (int i = 0; i < mailList.GetElementsByTagName("RecipientAddress").Count; i++)
                {
                    this.Add(mailList.GetElementsByTagName("RecipientAddress").Item(i).InnerText);
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Formats body as a html table
        /// </summary>
        /// <param name="duplicates">contains list of tuple type that contains all of ephone pairs that have duplicate MAC addresses</param>
        /// <returns>formatted body as a HTML table</returns>
        private StringBuilder formatMailBody(List<Tuple<MacAndCME, MacAndCME>> duplicates)
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("<strong>Following ephone pairs have duplicate MAC addresses: </strong><br><br>"
                + Environment.NewLine + Environment.NewLine);

            mailBody.Append("<table border = \"1\"><tr><td><strong>1ST EPHONE CME</strong></td><td><strong>"
                + "1ST EPHONE #</strong></td><td><strong>1ST EPHONE MAC</strong></td><td><strong>"
                + "2ND EPHONE CME</strong></td><td><strong>2ND EPHONE #</strong></td><td><strong>"
                + "2ND EPHONE MAC</strong></td></tr>");

            int rowsNumber = duplicates.Count;

            for (int i = 0; i < rowsNumber; i++)
            {

                mailBody.Append("<tr>");

                mailBody.Append("<td><strong>" + duplicates[i].Item1.CME + "</strong></td><td>" + duplicates[i].Item1.EPHONE
                    + "</td><td>" + duplicates[i].Item1.MAC + "</td><td><strong>" + duplicates[i].Item2.CME
                    + "</strong></td><td>" + duplicates[i].Item2.EPHONE + "</td><td>" + duplicates[i].Item2.MAC + "</td>");

                mailBody.Append("</tr>");
            }

            mailBody.Append("</table>");

            return mailBody;
        }
    }
}
