// Copyright (c) 2020 by RF77 (https://github.com/RF77)
// Licensed under the Apache 2.0 License.
// See LICENSE file in the project root for full license information.

using System;
using AsyncEnumerableExtensions.Tests;
using AsyncEnumerableMqttClient.Client;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using Nito.AsyncEx;
using Xunit.Abstractions;

namespace AsyncEnumerableMqttClient.Tests
{
	public class MqttTests : UnitTestBase
	{
		protected ManagedMqttClientOptions MqttOptions { get; }
		public const string TopicPrefix = "AsyncEnumerableMqttClientTests/";

		protected IAsyncEnumMqttClient MqttClient { get; }

		public MqttTests(ITestOutputHelper output) : base(output)
		{
			MqttClient = new MqttFactory().CreateAsyncEnumMqttClient();
			MqttOptions = new ManagedMqttClientOptions
			{
				ClientOptions = new MqttClientOptions
				{
					ClientId = $"ManagedMqttClientOptionsTest{Guid.NewGuid()}",
					ChannelOptions = new MqttClientTcpOptions
					{
						Server = "localhost"
						//Server = "broker.hivemq.com"
					}
				},

				AutoReconnectDelay = TimeSpan.FromSeconds(1),
			};
		}
		
		protected string GetTopic(string topicName) => $"{TopicPrefix}{topicName}";

	}
}