using RabbitMQ.Client;
using RabbitMQ.Client.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetMQ
{
    public static class ProducerMQ
    {
        public static void InitProducerMQ()
        {
            Uri uri = new Uri("amqp://127.0.0.1:5672/");
            string exchange = "ex1";
            string exchangeType = "direct";
            string routingKey = "m1";
            bool persistMode = true;
            ConnectionFactory cf = new ConnectionFactory();

            cf.UserName = "guest";
            cf.Password = "guest";
           //cf.VirtualHost = "dnt_mq";
            cf.RequestedHeartbeat = 0;
            cf.Endpoint = new AmqpTcpEndpoint(uri);
            using (IConnection conn = cf.CreateConnection())
            {
                using (IModel ch = conn.CreateModel())
                {
                    if (exchangeType != null)
                    {
                        ch.ExchangeDeclare(exchange, exchangeType);//,true,true,false,false, true,null);
                        ch.QueueDeclare("q1", false,false,false,null);//true, true, true, false, false, null);
                        ch.QueueBind("q1", "ex1", "m1", null);
                    }
                    var input = "";
                    Console.WriteLine("Enter a message. 'Quit' to quit.");
                    while ((input = Console.ReadLine()) != "Quit")
                    {
                        IMapMessageBuilder b = new MapMessageBuilder(ch);
                        IDictionary target = (IDictionary)b.Headers;
                        target["header"] = "hello world";
                        IDictionary targetBody = (IDictionary)b.Body;
                        targetBody["body"] = "daizhj";
                        if (persistMode)
                        {
                            ((IBasicProperties)b.GetContentHeader()).DeliveryMode = 2;
                        }

                        ch.BasicPublish(exchange, routingKey,
                                                   (IBasicProperties)b.GetContentHeader(),
                                                   b.GetContentBody());
                    }
                }
            }
        }
    }
}
