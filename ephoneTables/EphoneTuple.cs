using System.Collections.Generic;

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
        public EphoneTuple(KeyValuePair<string, RouterSectionItems> item1, 
            KeyValuePair<string, RouterSectionItems> item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}
