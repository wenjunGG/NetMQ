using EasyNetQ;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMQ
{
    public static class SendProduct
    {
        public static void sendPro()
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                var input = "";
                Console.WriteLine("Enter a message. 'Quit' to quit.");
                while ((input = Console.ReadLine()) != "Quit")
                {
                   // Send Receive
                    bus.Send("my.queue2", new TextMessage { Text = "Hello Widgets!" });
                }
            }
        }
    }
}
