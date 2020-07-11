using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncEnumerableExtensions.Karnok;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Receiving;
using MQTTnet.Diagnostics;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Internal;
using MQTTnet.Protocol;

namespace AsyncEnumerableMqttClient.Client
{
	public class AsyncEnumMqttClient : ManagedMqttClient, IAsyncEnumMqttClient
	{
		private readonly MulticastAsyncEnumerable<MqttApplicationMessage> _receivedMessages;

		private AsyncLock _asyncLock = new AsyncLock();

		public AsyncEnumMqttClient(IMqttClient mqttClient, IMqttNetLogger logger) : base(mqttClient, logger)
		{
			ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnReceivedMessage);
			_receivedMessages = new MulticastAsyncEnumerable<MqttApplicationMessage>();
		}

		public async Task SubscribeAsync(string topic,
			MqttQualityOfServiceLevel qos = MqttQualityOfServiceLevel.AtMostOnce)
		{
			await SubscribeAsync(new []{ new MqttTopicFilter() { QualityOfServiceLevel = qos, Topic = topic }});
		}

		public IAsyncEnumerable<MqttApplicationMessage> ReceivedMessages => _receivedMessages;

		public new async Task StopAsync()
		{
			// Wait, until all messages are published, at least if we're connected
			while (IsConnected && PendingApplicationMessagesCount > 0)
			{
				await Task.Delay(20);
			}

			// In my tests, the dispose method was not called on StopAsync => let's complete the received message here
			try
			{
				await _receivedMessages.Complete();
			}
			catch (Exception e)
			{
				await _receivedMessages.Error(e);
			}
		}

		public async Task WaitUntilConnectedAsync(CancellationToken cancellationToken = default)
		{
			// maybe there is a nicer way to wait.. just for now good enough
			while (!cancellationToken.IsCancellationRequested && ! IsConnected)
			{
				await Task.Delay(10, cancellationToken);
			}
		}

		private async void OnReceivedMessage(MqttApplicationMessageReceivedEventArgs args)
		{
			// What about error handling?
			try
			{
				using (await _asyncLock.WaitAsync())
				{
					Debug.WriteLine($"{args.ApplicationMessage.Topic}: {Encoding.UTF8.GetString(args.ApplicationMessage.Payload)}");
					if (!args.ProcessingFailed)
					{
						await _receivedMessages.Next(args.ApplicationMessage);
					}
				}
			}
			catch (Exception e)
			{
				await _receivedMessages.Error(e);
			}
		}
	}
}