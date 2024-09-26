using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "moura",
                Password = "Mour@505050"
            };
            using ( var connection = factory.CreateConnection()) 
            using ( var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Chat", 
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel); 
                    consumer.Received += (model, ea) =>  // delegates
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Mensagem recebida {0} ... ", message);
                    };

                    channel.BasicConsume(queue: "Chat", 
                                         autoAck: true, // Aviso que foi recebida a msg
                                         consumer: consumer);

                Console.WriteLine("Aperte [enter] para sair... ");
                Console.ReadLine();
                }

        }
    }
}
