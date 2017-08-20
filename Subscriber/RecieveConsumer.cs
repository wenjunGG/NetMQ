using EasyNetQ;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber
{
   public static class RecieveConsumer
    {
        public static void RecieveCon()
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                //sendrecieve
                bus.Receive<TextMessage>("my.queue2", message => Console.WriteLine("MyMessage: {0}", message.Text));
                Console.ReadLine();
            }
        }
    }
}
