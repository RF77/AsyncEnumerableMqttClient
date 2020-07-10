using System;
using MQTTnet;

namespace AsyncEnumerableMqttClient.Client
{
	public class BaseMessage : IBaseMessage
	{
		protected BaseMessage(MqttApplicationMessage message)
		{
			Message = message;
			TimeStamp = DateTime.Now;
		}

		public DateTime TimeStamp { get; }
		public MqttApplicationMessage Message { get; }

		public string Topic => Message.Topic;
		public bool Retain => Message.Retain;
	}
}