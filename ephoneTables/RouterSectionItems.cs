using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    class RouterSectionItems : Dictionary<string, string>
    {
        public RouterSectionItems(string[] fileContentArray, FTPFileModificationDate filename, string ephoneInstance)
        {
            foreach(string m in Regex.Matches(fileContentArray, @"\b" + ephoneInstance + @"\b"))
            {

            }


            /* number A0001
 name Conference
 conference ad-hoc*/
        }
    }
}
