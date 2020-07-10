using AsyncEnumerableExtensions.TimeOperators;

namespace AsyncEnumerableMqttClient.Client
{
	public interface IStringMessage<T> : IBaseMessage, ITimeStampItem<T>
	{
	}
}