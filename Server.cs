using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Client_Server
{
    public class Server
    {
        private static readonly List<Cars> CarsList = new List<Cars>
        {
            new() {Brand = "Toyota", Year = 2001, VolueEngine = 2.5f},
            new() {Brand = "VV", Year = 2008, VolueEngine = 2.0f},
            new() {Brand = "Nissan", Year = 2016, VolueEngine = 3.2f}
        };

        

        private Socket socket;

        public Server() 
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 7777);
            socket.Bind(ipPoint);
            socket.Listen(10);
            Console.WriteLine(socket.LocalEndPoint + "Подключение");
        }

        public Task StartAsync() 
        {
          
            return Task.Run(() =>
            {
                var client = socket.Accept();
                var carsByte = GetCarsByte(CarsList);
                client.Send(carsByte);
                client.Close();
            });
        }

        private byte[] GetCarsByte(List<Cars> carsList)
        {
            var bytes = new List<byte>();
            foreach (var cars in carsList)
            {
                bytes.AddRange(GetCarByte(cars));
            }
            return bytes.ToArray();
        }





        private static byte[] GetCarByte(Cars cars) 
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(0x02);
            bytes.Add(0x03);
            var brandBytes = Encoding.ASCII.GetBytes(cars.Brand);
            bytes.Add(0x09);
            bytes.Add((byte)brandBytes.Length);
            bytes.AddRange(brandBytes);
            bytes.Add(0x12);
            bytes.AddRange(BitConverter.GetBytes(cars.Year));
            bytes.Add(0x13);
            bytes.AddRange(BitConverter.GetBytes(cars.VolueEngine));

            return bytes.ToArray();
        }
        


    }
}
