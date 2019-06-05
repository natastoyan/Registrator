using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using System.Threading;

namespace Registrator.Bus.Core
{
	public class Sender
	{
		private ConnectionFactory _factory;
		private IConnection _connection;
		public IModel Channel;

		//private Sender(string hostName, string queueName)
		public Sender(string hostName = "localhost")
		{
			_factory = new ConnectionFactory() { HostName = hostName };
			_connection = _factory.CreateConnection();
			Channel = _connection.CreateModel();
		}



		//TODO не хардкодить адрес 
		public string CreateMessage(Byte[] body, string queueName)
		{
			//var sender = new Sender(hostName: "localhost", queueName: "hello");

			string message = "";
			try
			{
				using (var channel = Channel)
				{
					channel.QueueDeclare(queue: queueName,
						 durable: false,
						 exclusive: false,
						 autoDelete: false,
						 arguments: null);

					message = DateTime.Now.ToString() + " " + Encoding.UTF8.GetString(body);

					channel.BasicPublish(exchange: "",
										 routingKey: queueName,
										 basicProperties: null,
										 body: body);
					Console.WriteLine("[x] Sent \" {0}\"", message);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}




			return message;
			//var factory = new ConnectionFactory() { HostName = "localhost" };
			//using (var connection = factory.CreateConnection())
			//{
			//	using (var channel = connection.CreateModel())
			//	{
			//		channel.QueueDeclare(queue: "hello",
			//							 durable: false,
			//							 exclusive: false,
			//							 autoDelete: false,
			//							 arguments: null);

			//		for (var i = 0; i < 3; i++)
			//		{
			//			string message = DateTime.Now.ToString() + " Hello RabbitMQ!";
			//			body = Encoding.UTF8.GetBytes(message);
			//			channel.BasicPublish(exchange: "",
			//								 routingKey: "hello",
			//								 basicProperties: null,
			//								 body: body);
			//			Console.WriteLine("[x] Sent \"{0}\"", message);

			//			Thread.Sleep(5000);

			//		}


			//	}

			//public void CreateQueue(string queueName = "hello")
			//{
			//	try
			//	{
			//		using (var channel = Channel)
			//		{
			//			channel.QueueDeclare(queue: queueName,
			//									 durable: false,
			//									 exclusive: false,
			//									 autoDelete: false,
			//									 arguments: null);
			//		}
			//	}
			//	catch(Exception ex)
			//	{
			//		Console.WriteLine(ex.Message);
			//	}

			//}

		}
	}

}
