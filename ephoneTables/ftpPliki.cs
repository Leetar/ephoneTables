using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ephoneTables
{
    public class ftpPliki : List<ftpPlik>
    {
       
        public ftpPliki(string ftp_ipaddr, int ftp_port)
        {
            // laczenie do ftp
            // sciagniecie lini
            foreach (string line in lines)
            {
                this.Add(new ftpPlik(line));
            }
        }

        public IEnumerable<ftpPlik> get_sort()
        {
            return this.OrderBy(x => x.modificationDate);
        }
    }
}*/
