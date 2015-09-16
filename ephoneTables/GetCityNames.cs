using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ephoneTables
{
    class GetCityNames : Dictionary<string, string>
    {
        public void GetCityNamesDict()
        {
            XmlDocument xmlCitiesTranslationList = new XmlDocument();
            try
            {
                var folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var configPath = Path.Combine(folderPath, "CityCodeTranslation.xml");

                xmlCitiesTranslationList.Load(configPath);
                int elementsCount = xmlCitiesTranslationList.GetElementsByTagName("city").Count;

                for (int i = 0; i < elementsCount; i++)
                {
                    this.Add(xmlCitiesTranslationList.GetElementsByTagName("code").Item(i).InnerText, 
                        xmlCitiesTranslationList.GetElementsByTagName("name").Item(i).InnerText);
                }

            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
