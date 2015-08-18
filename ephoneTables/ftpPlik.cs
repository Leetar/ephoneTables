using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ephoneTables
{
    public class ftpPlik
    {
        public DateTime modificationDate
        {
            get;
            private set;
        }
        public string CME
        {
            get;
            private set;
        }

        public ftpPlik(string line)
        {
            // parsowanie lini
            // wykonwanie requesta
            // uzupelnianie cme i modification date
        }
    }
}
