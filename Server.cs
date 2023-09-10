﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server
{
    public class Server
    {
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
            var car = CarsDataFrom();
            return Task.Run(() =>
            {
                var client = socket.Accept();
                var carsByte = GetCarByte(car);
                client.Send(carsByte);
                client.Close();
            });
        }

        
        
        private Cars CarsDataFrom()
        {
            List<Cars> cars = new List<Cars>()
            {
                new Cars { Brand = "Toyota", Year = 2001, VolueEngine = 2.5f },
                new Cars { Brand = "VV", Year = 2008, VolueEngine = 2.0f },
                new Cars { Brand = "Nissan", Year = 2016, VolueEngine = 3.2f},
            };



        }

    }

        private byte[] GetCarByte(Cars cars) 
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
