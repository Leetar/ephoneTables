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
        public RouterSectionItems(string[] sectionContent) // przekazywać stringa z już wydzielona sekcją
        {            
            foreach(string inSectionLine in sectionContent)
            {
                string line = inSectionLine.Trim();

                string[] tkns = line.Split(' ');
                string name = tkns[0];
                List<string> tknsList = new List<string>(tkns);
                tknsList.RemoveAt(0);
                string value = string.Join(" ", tknsList.ToArray()).Trim();

                if (this.ContainsKey(name)) { continue; }
                this.Add(name, value);
                
                                
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
