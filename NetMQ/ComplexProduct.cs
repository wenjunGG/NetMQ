using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace NetMQ
{
   public static class ComplexProduct
    {
        public static string EXCHANGE_TYPE = "fanout";
        public static string EXCHANGE_NAME = "exchange_famout1";
        public static string CHANNEL_NAME = "chanel_fanout1";
        public static string ROUTR_NAME = "route_fanout1";

        public  static void ComplexPro()
        {
            ConnectionFactory Factory = new ConnectionFactory()
            {
                UserName="guest",
                Password="guest",
                Port=5672,
                HostName="127.0.0.1",
                Protocol = Protocols.DefaultProtocol,
                AutomaticRecoveryEnabled = true, //自动重连  
                RequestedFrameMax = UInt32.MaxValue,
                RequestedHeartbeat = UInt16.MaxValue //心跳超时时间  
            };

            using (IConnection Connect = Factory.CreateConnection())
            {
                using (IModel channel = Connect.CreateModel())
                {
                    channel.ExchangeDeclare(EXCHANGE_NAME, EXCHANGE_TYPE, true, false, null);
                    channel.QueueDeclare(CHANNEL_NAME, true, false, false, null);
                    channel.QueueBind(CHANNEL_NAME, EXCHANGE_NAME, ROUTR_NAME);


                    // 设置消息属性  
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; //消息是持久的，存在并不会受服务器重启影响   

                    var input = "";
                    Console.WriteLine("start");
                    while((input= Console.ReadLine())!="quit")
                    {
                        var mess=Encoding.UTF8.GetBytes(input);
                        channel.BasicPublish(EXCHANGE_NAME, ROUTR_NAME, properties, mess);
                    }
                }
            }
        }
    }
}
