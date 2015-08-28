using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ephoneTables
{
    class EphoneTuple
    {
        public KeyValuePair<string, RouterSectionItems> Item1
        {
            get;
            private set;
        }
        public KeyValuePair<string, RouterSectionItems> Item2
        {
            get;
            private set;
        }
        public EphoneTuple(KeyValuePair<string, RouterSectionItems> _item1, 
            KeyValuePair<string, RouterSectionItems> _item2)
        {
            Item1 = _item1;
            Item2 = _item2;
        }
    }
}
