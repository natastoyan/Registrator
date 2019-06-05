using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Registrator.Bus.Core
{
	public class Receiver
	{
		private ConnectionFactory _factory;
		private IConnection _connection;
		public IModel Channel;

		public Receiver(string hostName = "localhost")
		{
			_factory = new ConnectionFactory() { HostName = hostName };
			_connection = _factory.CreateConnection();
			Channel = _connection.CreateModel();
		}

		public void Listen(string queueName)
		{
			using (var channel = Channel)
			{
				channel.QueueDeclare(queue: queueName,
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);

				var consumer = new EventingBasicConsumer(channel);
					consumer.Received += (model, ea) =>
					{
						var body = ea.Body;
						var message = Encoding.UTF8.GetString(body);
						Console.WriteLine(" [x] Received \"{0}\" on queue: {1}", message, queueName);
					};

					channel.BasicConsume(queue: "hello",
										autoAck: true,
										consumer: consumer);

					Console.WriteLine(" Press [enter] to exit.");
					Console.ReadLine();
				}
			}
		}
}

