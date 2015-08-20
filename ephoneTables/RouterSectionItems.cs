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
        public RouterSectionItems(string[] sectionContent) //przekazywać stringa z już wydzielona sekcją
        {
            foreach(string inSectionLine in sectionContent)
            {

            }

            //string[] fileContentArray = fileContent.Split('\n');
            
            //foreach(Match m in Regex.Matches(fileContent, @"\b" + ephoneInstance + @"\b"))
            //{
            //    IEnumerable<string> sections = fileContent.IndexOf(ephoneInstance);
            //    Console.WriteLine(sections);
            //}


            /* number A0001
 name Conference
 conference ad-hoc*/
        }
    }
}
