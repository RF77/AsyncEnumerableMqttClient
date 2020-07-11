using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;

namespace AsyncEnumerableMqttClient.Client
{
	public interface IAsyncEnumMqttClient : IManagedMqttClient
	{
		Task SubscribeAsync(string topic,
			MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce);

		IAsyncEnumerable<MqttApplicationMessage> ReceivedMessages { get; }

		new Task StopAsync();

		Task WaitUntilConnectedAsync(CancellationToken cancellationToken = default);
	}
}