using System;
using System.Text;
using MQTTnet;

namespace AsyncEnumerableMqttClient.Client
{
	/// <summary>
	/// Payload is a string and will be directly converted to the target value
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class StringMessage<T> : BaseMessage, IStringMessage<T>
	{
		public T Content { get; }

		protected StringMessage(MqttApplicationMessage message) : base(message)
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
						content = (T) Convert.ChangeType(text, typeof(T));
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