using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client_Server
{
    [Serializable]
    public class Cars 
    {
        public string Brand {get;set;}
        public ushort Year { get;set;}
        public float VolueEngine { get;set;}

        

        public void SaveCarToXML(Cars cars, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cars));
            using (FileStream fs = new FileStream("cars.xml", FileMode.OpenOrCreate))
                serializer.Serialize(fs, cars);
            Console.WriteLine("Данные сохранены в файл 'cars.xml'");
        }

    }

}
