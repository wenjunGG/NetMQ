using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Subscriber
{
    public static class CustmerMq
    {
        public static void InitCustmerMq()
        {
            string exchange = "ex1";
            string exchangeType = "direct";
            string routingKey = "m1";

            Uri uri = new Uri("amqp://127.0.0.1:5672/");
            ConnectionFactory cf = new ConnectionFactory();
            cf.Endpoint = new AmqpTcpEndpoint(uri);
            cf.UserName = "guest";
            cf.Password = "guest";
            // cf.VirtualHost = "dnt_mq";
            cf.RequestedHeartbeat = 0;
            using (IConnection conn = cf.CreateConnection())
            {
                using (IModel ch = conn.CreateModel())
                {
                    //普通使用方式BasicGet
                    //noAck = true，不需要回复，接收到消息后，queue上的消息就会清除
                    //noAck = false，需要回复，接收到消息后，queue上的消息不会被清除，直到调用channel.basicAck(deliveryTag, false); queue上的消息才会被清除 而且，在当前连接断开以前，其它客户端将不能收到此queue上的消息
                    //BasicGetResult res = ch.BasicGet("q1", false/*noAck*/);
                    //if (res != null)
                    //{
                    //    bool t = res.Redelivered;
                    //    t = true;
                    //    Console.WriteLine(System.Text.UTF8Encoding.UTF8.GetString(res.Body));
                    //    ch.BasicAck(res.DeliveryTag, false);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("No message！");
                    //}

                    ch.ExchangeDeclare(exchange, exchangeType);//,true,true,false,false, true,null);
                    ch.QueueDeclare("q1", false, false, false, null);//true, true, true, false, false, null);
                    ch.QueueBind("q1", "ex1", "m1", null);

                    //while (true)
                    //{
                    //    BasicGetResult res = ch.BasicGet("q1", false/*noAck*/);
                    //    if (res != null)
                    //    {
                    //        try
                    //        {
                    //            bool t = res.Redelivered;
                    //            t = true;
                    //            Console.WriteLine(System.Text.UTF8Encoding.UTF8.GetString(res.Body));
                    //            ch.BasicAck(res.DeliveryTag, false);
                    //        }
                    //        catch { }
                    //    }
                    //    else
                    //        break;
                    //}


                    QueueingBasicConsumer consumer = new QueueingBasicConsumer(ch);
                    ch.BasicConsume("q1", false, consumer);
                    while (true)
                    {
                        try
                        {
                            BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            IBasicProperties props = e.BasicProperties;
                            byte[] body = e.Body;
                            Console.WriteLine(System.Text.Encoding.UTF8.GetString(body));
                            ch.BasicAck(e.DeliveryTag, true);
                            //  ProcessRemainMessage();
                        }
                        catch (EndOfStreamException ex)
                        {
                            //The consumer was removed, either through channel or connection closure, or through the action of IModel.BasicCancel(). 
                            Console.WriteLine(ex.ToString());
                            break;
                        }
                        Console.ReadLine();
                    }
                }

            }
        }
    }
}
        