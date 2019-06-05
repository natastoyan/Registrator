using System;
using System.Collections.Generic;
using System.Text;

namespace Registrator.Bus.Core
{
	class Program
	{
		public static void Main()
		{
			var sender = new Sender();
			var message = Encoding.UTF8.GetBytes("Hello RabbitMQ");
			sender.CreateMessage(message, "hello");
			sender.CreateMessage(message, "goodbye");

			var receiver = new Receiver();
			receiver.Listen("hello");

			var reciever2 = new Receiver();
			receiver.Listen("goodbye");

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}
	}
}
