using AsyncEnumerableExtensions.TimeOperators;

namespace AsyncEnumerableMqttClient.Client
{
	public interface IByteArrayMessage : IBaseMessage, ITimeStampItem<byte[]>
	{

	}
}