using EasyNetQ;
using Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Subscriber
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
            //    //bus.Subscribe<TextMessage>("test11", HandleTextMessage);

            //    //Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    //Console.ReadLine();

            //    //异步
            //    //    bus.SubscribeAsync<TextMessage>("subscribe_async_test", message =>
            //    //   new WebClient().DownloadStringTask(new Uri("http://localhost:1338/?timeout=500"))
            //    //.ContinueWith(task =>
            //    //    Console.WriteLine("Received: '{0}', Downloaded: '{1}'",
            //    //        message.Text,
            //    //        task.Result)));

            //    //请求响应
            //    //bus.Respond<TextMessage, TextMessage>(request => 
            //    //new TextMessage { Text = "Responding to " + HandleTextMessage(request.Text) });

            //sendrecieve
            //    bus.Receive<TextMessage>("my.queue1", message => Console.WriteLine("MyMessage: {0}", message.Text));
            //    Console.ReadLine();
            //}

            //            var factory = new ConnectionFactory() { HostName = "localhost" };
            //            using (var connection = factory.CreateConnection())
            //            {
            //                using (var channel = connection.CreateModel())
            //                {
            //                    channel.QueueDeclare("hello", false, false, false, null);
            //                    var consumer = new QueueingBasicConsumer(channel);

            //#if demo1
            //                     channel.BasicConsume("hello", true, consumer);//自动删除消息
            //#else
            //                    channel.BasicConsume("hello", true, consumer);//需要接受方发送ack回执,删除消息
            //#endif
            //                    Console.WriteLine(" [*] Waiting for messages." + "To exit press CTRL+C");
            //                    while (true)
            //                    {
            //                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();//挂起的操作
            //#if demo2
            //                        channel.BasicAck(ea.DeliveryTag, false);//与channel.BasicConsume("hello", false, null, consumer);对应
            //#endif
            //                        var body = ea.Body;
            //                        var message = Encoding.UTF8.GetString(body);
            //                        Console.WriteLine(" [x] Received {0}", message);
            //                        int dots = message.Split('.').Length - 1;
            //                        Thread.Sleep(dots * 1000);
            //                        Console.WriteLine(" [x] Done");
            //#if demo2
            //                         //channel.BasicAck(ea.DeliveryTag, false);//与channel.BasicConsume("hello", false, null, consumer);对应,这句话写道40行和49行运行结果就会不一样.写到这里会发生如果输出[x] Received {0}之后,没有输出 [x] Done之前,CTRL+C结束程序,那么message会自动发给另外一个客户端,当另外一个客户端收到message后,执行完49行的命令之后,服务器会删掉这个message
            //#endif
            //                    }
            //                }
            //  }


            ////广播模式
            //ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            //using (IConnection Connection = factory.CreateConnection())
            //{
            //    using (IModel channel = Connection.CreateModel())
            //    {
            //        channel.ExchangeDeclare(EXCHANGE_NAME, EXCHANGE_TYPE);
            //       // channel.QueueDeclare(CHANCEL_NAME, false);//true, true, true, false, false, null);
            //      channel.QueueBind(CHANCEL_NAME, EXCHANGE_NAME, ROUTEKEY);

            //       channel.QueueDeclare(CHANCEL_NAME, false, false, false, null);

            //        var consumer = new QueueingBasicConsumer(channel);

            //        channel.BasicConsume(CHANCEL_NAME, false, consumer);

            //        Console.WriteLine(" [*] Waiting for messages." + "To exit press CTRL+C");
            //        while (true)
            //        {
            //            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();//挂起的操作
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine(" [x] Received {0}", message);
            //        }
            //    }
            //}

            //CustmerMq.InitCustmerMq();

            //广播 success
             Consumer.Con();

            //最简单的模式
            // SimConsumer.SimCon();

            //send recieve
            // RecieveConsumer.RecieveCon();

            //复杂
           // ComplexConsumer.ComplexCon();
        }





        static string HandleTextMessage(string mess)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", mess);
            Console.ResetColor();
            return "ok";
        }
    }
}