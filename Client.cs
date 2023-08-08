using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace Client_Server
{
    public class Client
    {
        private Socket socket;
        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Loopback, 7777);
            socket.Connect(ipPoint);
            Console.WriteLine("Connect to server");
        }

        public Cars GetCars()
        {
            var buf = new byte[1024];
            socket.Receive(buf);
            return ParseCar(buf);
        }

        private Cars ParseCar(byte[] bytes)
        {
            int i = 0;
            if (bytes[i++] != 0x02 || bytes[i++] != 0x03)
            {
                throw new Exception("Неверные данные");
            }
            i++;
            int brandLength = bytes[i++];
            var brand = Encoding.ASCII.GetString(bytes, i, brandLength);
            i += brandLength;

            i++;
            var year = BitConverter.ToInt32(bytes, i);
            i += 2;

            i++;
            var volueEngine = BitConverter.ToSingle(bytes, i);
            return new Cars { Brand = brand, Year = (ushort)year, VolueEngine = volueEngine };
        }

        public void SaveCarToXML(Cars cars, string filename)
        {
            var serializer = new XmlSerializer(typeof(Cars));
            using var fs = new StreamWriter(filename);
            serializer.Serialize(fs, cars);
            Console.WriteLine("Данные сохранены в файл 'cars.xml'");
        }



    }
}
