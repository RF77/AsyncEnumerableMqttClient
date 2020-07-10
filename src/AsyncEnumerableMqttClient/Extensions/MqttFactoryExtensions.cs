using System;
using AsyncEnumerableMqttClient;
using AsyncEnumerableMqttClient.Client;
using MQTTnet;
using MQTTnet.Diagnostics;

namespace MQTTnet.Extensions.ManagedClient
{
	public static class MqttFactoryExtensions
	{
		public static IAsyncEnumMqttClient CreateAsyncEnumMqttClient(this IMqttFactory factory)
		{
			if (factory == null) throw new ArgumentNullException(nameof(factory));

			return new AsyncEnumMqttClient(factory.CreateMqttClient(), factory.DefaultLogger);
		}

		public static IAsyncEnumMqttClient CreateAsyncEnumMqttClient(this IMqttFactory factory, IMqttNetLogger logger)
		{
			if (factory == null) throw new ArgumentNullException(nameof(factory));
			if (logger == null) throw new ArgumentNullException(nameof(logger));

			return new AsyncEnumMqttClient(factory.CreateMqttClient(logger), logger);
		}
	}
}