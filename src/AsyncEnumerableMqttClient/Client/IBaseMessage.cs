using MQTTnet;

namespace AsyncEnumerableMqttClient.Client
{
	public interface IBaseMessage
	{
		MqttApplicationMessage Message { get; }
		string Topic { get; }
		bool Retain { get; }
	}
}