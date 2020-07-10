using AsyncEnumerableExtensions.TimeOperators;

namespace AsyncEnumerableMqttClient.Client
{
	public interface IJsonMessage<T> : IBaseMessage, ITimeStampItem<T>
	{
	}
}