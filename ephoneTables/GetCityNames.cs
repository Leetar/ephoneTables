using System;
using System.Collections.Generic;
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
                xmlCitiesTranslationList.Load("CityCodeTranslation.xml");
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
