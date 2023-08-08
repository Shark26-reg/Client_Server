using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();

            var client = new Client();
            server.StartAsync();
            Console.WriteLine("Данные отправлены");

            var cars = client.GetCars();

            client.SaveCarToXML(cars, "cars.xml");
            Console.WriteLine("Данные сохранены");
        }
    }

}