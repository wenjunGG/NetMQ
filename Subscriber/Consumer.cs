using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Subscriber
{
    public static class Consumer
    {
        public static void Con()
        {
            // 建立RabbitMQ连接和通道  
            var connectionFactory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                Protocol = Protocols.AMQP_0_9_1,
                RequestedFrameMax = UInt32.MaxValue,
                RequestedHeartbeat = UInt16.MaxValue
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // 这指示通道不预取超过1个消息  
                channel.BasicQos(0, 1, false);

                //创建一个新的，持久的交换区  
                channel.ExchangeDeclare("SISOExchange_fanout", ExchangeType.Fanout, true, false, null);
                //创建一个新的，持久的队列  
                channel.QueueDeclare("sample-queue_fanout", true, false, false, null);
                //绑定队列到交换区  
                channel.QueueBind("SISOqueue_fanout", "SISOExchange_fanout", "optionalRoutingKey_fanout");


                using (var subscription = new Subscription(channel, "SISOqueue_fanout", false))
                {
                    Console.WriteLine("等待消息...");
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
                //channel.BasicConsume("SISOqueue_fanout", false, consumer);
                //while (true)
                //{
                //    try
                //    {
                //        BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                //        IBasicProperties props = e.BasicProperties;
                //        byte[] body = e.Body;
                //        Console.WriteLine(System.Text.Encoding.UTF8.GetString(body));
                //       // channel.BasicAck(e.DeliveryTag, true);
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
