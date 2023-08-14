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

            Console.WriteLine("####Menu###");
            Console.WriteLine("1. Вывести все авто  ");
            Console.WriteLine("2. Выход ");
            Console.Write("\n" + "Введите команду: ");
            Console.WriteLine();
            string str = Console.ReadLine();
            switch(str)
            {
                case "1": client.ShowCar(cars);
                    break;
                case "2": 
                    return;
                    
       
            }

            client.SaveCarToXML(cars, "cars.xml");
        }
    }

}