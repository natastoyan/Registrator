using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Registrator.Bus.Core;

namespace Registrator.Tests.Bus.Core
{
	public class Sender_DoesSendMessage
	{
		private readonly Sender _sender;

		public Sender_DoesSendMessage()
		{
			_sender = new Sender();
		}

		[Fact]
		public void ReturnFalse()
		{
			var result = _sender.CreateMessage(Encoding.UTF8.GetBytes("Hello RabbitMQ"));
		}
	}
}
