using EasyNetQ;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMQ
{
    public static class Simproduct
    {
        public static void sim()
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var input = "";
                Console.WriteLine("Enter a message. 'Quit' to quit.");
                while ((input = Console.ReadLine()) != "Quit")
                {
                    bus.Publish(new TextMessage
                    {
                        Text = input
                    });
                }
            }
        }
    }
}
