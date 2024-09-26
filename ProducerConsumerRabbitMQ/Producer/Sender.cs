using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Channels;


var factory = new ConnectionFactory() //Criação de uma factory
{
    HostName = "localhost",
    Port = 5672,
    UserName = "moura",
    Password = "Mour@505050"
}; 

using (var connection = factory.CreateConnection())  // Abriu uma conexão
using (var channel = connection.CreateModel()) // Abriu um canal
{
    channel.QueueDeclare(queue: "Chat",        // Declarou a fila     
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    Console.WriteLine("Digite sua mensagem e aperte <ENTER>"); // Criou uma menssagem

    var messages = new List<string>();

    while (true)
    {
        string message = Console.ReadLine();

        if (message == "")
            break;

        messages.Add(message);

    }

    foreach (var msg in messages)
    {

    
        var body = Encoding.UTF8.GetBytes(s:msg);

        channel.BasicPublish(exchange: string.Empty,
                                       routingKey: "Chat",
                                       basicProperties: null,
                                       body: body);

        Console.WriteLine($"[x] Enviando {msg}");

        Console.WriteLine("Aperte [enter] para sair...");
        Console.ReadLine();
    }
}
    

