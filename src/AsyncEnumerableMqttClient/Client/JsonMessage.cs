using System;
using System.Text;
using MQTTnet;
using Newtonsoft.Json;

namespace AsyncEnumerableMqttClient.Client
{
	public class JsonMessage<T> : BaseMessage, IJsonMessage<T>
	{
		public T Content { get; }

		protected JsonMessage(MqttApplicationMessage message) : base(message)
		{
			T content = default;
			var payload = message.Payload;

			if (payload != null)
			{
				var text = Encoding.UTF8.GetString(payload);
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						content = JsonConvert.DeserializeObject<T>(text);
					}
					catch
					{
						// TODO exception handling?
					}
				}
			}

			Content = content;
		}
	}
}