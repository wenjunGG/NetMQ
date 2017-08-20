using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;
using RabbitMQ.Client.MessagePatterns;

namespace Subscriber
{
   public static  class ComplexConsumer
    {
        public static string EXCHANGE_TYPE = "fanout";
        public static string EXCHANGE_NAME = "exchange_famout1";
        public static string CHANNEL_NAME = "chanel_fanout1";
        public static string ROUTR_NAME = "route_fanout1";
        public static void ComplexCon()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName="127.0.0.1",
                Port=5672,
                UserName="guest",
                Password="guest",
                Protocol = Protocols.DefaultProtocol,
                AutomaticRecoveryEnabled = true, //自动重连  
                RequestedFrameMax = UInt32.MaxValue,
                RequestedHeartbeat = UInt16.MaxValue //心跳超时时间  

            };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    // 这指示通道不预取超过1个消息  
                    channel.BasicQos(0, 1, false);

                    channel.ExchangeDeclare(EXCHANGE_NAME, EXCHANGE_TYPE, true, false, null);
                    channel.QueueDeclare(CHANNEL_NAME, true, false, false, null);
                    channel.QueueBind(CHANNEL_NAME, EXCHANGE_NAME, ROUTR_NAME);


                    using (var subscription = new Subscription(channel, CHANNEL_NAME, false))
                    {
                        Console.WriteLine("wait...");
                        var encoding = new UTF8Encoding();
                        while (channel.IsOpen)
                        {
                            BasicDeliverEventArgs eventArgs;
                            var success = subscription.Next(2000, out eventArgs);
                            if (success == false) continue;
                            var msgBytes = eventArgs.Body;
                            var message = encoding.GetString(msgBytes);
                            Console.WriteLine(message);
                            channel.BasicAck(eventArgs.DeliveryTag, false);
                        }
                    }

                    //QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                    //channel.BasicConsume("CHANNEL_NAME", false, consumer);
                    //while (true)
                    //{
                    //    try
                    //    {
                    //        BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    //        IBasicProperties props = e.BasicProperties;
                    //        byte[] body = e.Body;
                    //        Console.WriteLine(System.Text.Encoding.UTF8.GetString(body));
                    //        // channel.BasicAck(e.DeliveryTag, true);
                    //        //  ProcessRemainMessage();
                    //    }
                    //    catch (EndOfStreamException ex)
                    //    {
                    //        //The consumer was removed, either through channel or connection closure, or through the action of IModel.BasicCancel(). 
                    //        Console.WriteLine(ex.ToString());
                    //        break;
                    //    }
                    //    Console.ReadLine();
                    //}

                }
            }
        }
    }
}
