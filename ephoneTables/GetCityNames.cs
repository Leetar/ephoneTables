using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ephoneTables
{
    class GetCityNames : Dictionary<string, string>
    {
        public void GetCityNamesDict()
        {
            XmlDocument XmlCitiesTranslationList = new XmlDocument();
            try
            {
                XmlCitiesTranslationList.Load("CityCodeTranslation.xml");
                int elementsCount = XmlCitiesTranslationList.GetElementsByTagName("city").Count;

                Dictionary<string, string> citiesList = new Dictionary<string, string>();

                for (int i = 0; i < elementsCount; i++)
                {
                    this.Add(XmlCitiesTranslationList.GetElementsByTagName("code").Item(i).InnerText, 
                        XmlCitiesTranslationList.GetElementsByTagName("name").Item(i).InnerText);
                }

            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
