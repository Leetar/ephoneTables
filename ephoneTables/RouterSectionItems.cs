using System.Collections.Generic;

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
        }
    }
}
