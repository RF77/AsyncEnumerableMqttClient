using MQTTnet;

namespace AsyncEnumerableMqttClient.Client
{
	/// <summary>
	/// Plain Byte Array
	/// </summary>
	public class ByteArrayMessage : BaseMessage, IByteArrayMessage
	{
		public ByteArrayMessage(MqttApplicationMessage message):base(message)
		{
			Content = message.Payload;
		}

		public byte[] Content { get; }
	}
}