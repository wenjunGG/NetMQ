using EasyNetQ;
using Model;
using RabbitMQ.Client;
using System;
using System.Text;

namespace NetMQ
{
    class Program
    {
        private static string EXCHANGE_NAME = "exchangefanout";
        private static string ROUTEKEY = "k1fanout";
        private static string CHANCEL_NAME = "chanelfamout";
        private static string EXCHANGE_TYPE = "fanout";
        static void Main(string[] args)
        {
            //using (var bus = RabbitHutch.CreateBus("host=localhost"))
            //{
            //    var input = "";
            //    Console.WriteLine("Enter a message. 'Quit' to quit.");
            //    while ((input = Console.ReadLine()) != "Quit")
            //    {
            //    //bus.Publish(new TextMessage
            //    //{
            //    //    Text = input
            //    //});

            //    ////请求与响应
            //    //var myRequest = new TextMessage { Text = input };
            //    //var response = bus.Request<TextMessage, TextMessage>(myRequest);
            //    //Console.WriteLine(response.Text);

            //Send Receive
            //        bus.Send("my.queue1", new TextMessage { Text = "Hello Widgets!" });
            //    }
            //}
            //ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            //using (IConnection connection = factory.CreateConnection())
            //{
            //    using (IModel channel = connection.CreateModel())
            //    {
            //        channel.QueueDeclare("hello", false, false, false, null);

            //        var input = "";
            //        Console.WriteLine("Enter a message. 'Quit' to quit.");
            //        while ((input = Console.ReadLine()) != "Quit")
            //        {
            //            var message = GetMessage(args);
            //            var body = Encoding.UTF8.GetBytes(message);

            //            var properties = channel.CreateBasicProperties();
            //            properties.DeliveryMode = 2;//non-persistent (1) or persistent (2) 
            //                                        //channel.TxSelect();

            //            channel.BasicPublish("", "hello", properties, body);
            //            //channel.TxCommit();
            //        }
            //    }
            //}


            ////广播模式模式
            //ConnectionFactory factory = new ConnectionFactory();
            //using (IConnection connection = factory.CreateConnection())
            //{
            //    using (IModel channel = connection.CreateModel())
            //    {
            //        channel.ExchangeDeclare(EXCHANGE_NAME, EXCHANGE_TYPE);

            //       // channel.QueueDeclare(CHANCEL_NAME, false);//true, true, true, false, false, null);
            //        channel.QueueDeclare(CHANCEL_NAME, false, false, false, null);

            //        channel.QueueBind(CHANCEL_NAME, EXCHANGE_NAME, ROUTEKEY, null);

            //        var input = "";
            //             Console.WriteLine("Enter a message. 'Quit' to quit.");
            //        while ((input = Console.ReadLine()) != "Quit")
            //        {

            //            string msg = DateTime.Now + " have log sth...";
            //            var body = Encoding.UTF8.GetBytes(msg);


            //            channel.BasicPublish(EXCHANGE_NAME, "", null, body);

            //        }
            //    }
            // }

            // ProducerMQ.InitProducerMQ();

            //广播模式 success
            product.pro();


            //最简单模式订阅发布
            // Simproduct.sim();

            //send recieve
            // SendProduct.sendPro();

            //复杂一点
          // ComplexProduct.ComplexPro();

        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}