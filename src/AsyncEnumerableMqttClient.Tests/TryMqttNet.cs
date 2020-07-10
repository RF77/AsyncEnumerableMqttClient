using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using Xunit;

namespace AsyncEnumerableMqttClient.Tests
{
	public class TryMqttNet
	{
		[Fact]
		public async Task Test1()
		{
			var options = new ManagedMqttClientOptionsBuilder()
				.WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
				.WithClientOptions(new MqttClientOptionsBuilder()
					.WithClientId("Client1")
					.WithTcpServer("broker.hivemq.com")
					.WithTls().Build())
				.Build();

			IManagedMqttClient mqttClient = new MqttFactory().CreateManagedMqttClient();
			await mqttClient.SubscribeAsync(new MqttTopicFilter{});
			await mqttClient.StartAsync(options);

		}
	}
}
