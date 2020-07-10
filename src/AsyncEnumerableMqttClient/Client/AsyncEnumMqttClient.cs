using System;
using System.Collections.Generic;
using AsyncEnumerableExtensions.Karnok;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Receiving;
using MQTTnet.Diagnostics;
using MQTTnet.Extensions.ManagedClient;

namespace AsyncEnumerableMqttClient.Client
{
	public class AsyncEnumMqttClient : ManagedMqttClient, IAsyncEnumMqttClient
	{
		private readonly MulticastAsyncEnumerable<MqttApplicationMessage> _receivedMessages;

		public AsyncEnumMqttClient(IMqttClient mqttClient, IMqttNetLogger logger) : base(mqttClient, logger)
		{
			ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnReceivedMessage);
			_receivedMessages = new MulticastAsyncEnumerable<MqttApplicationMessage>();
		}

		public IAsyncEnumerable<MqttApplicationMessage> ReceivedMessages => _receivedMessages;

		protected override async void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!IsDisposed)
			{
				try
				{
					await _receivedMessages.Complete();
				}
				catch (Exception e)
				{
					await _receivedMessages.Error(e);
				}
			}
		}

		private async void OnReceivedMessage(MqttApplicationMessageReceivedEventArgs args)
		{
			// What about error handling?
			try
			{
				if (!args.ProcessingFailed)
				{
					await _receivedMessages.Next(args.ApplicationMessage);
				}
			}
			catch (Exception e)
			{
				await _receivedMessages.Error(e);
			}
		}
	}
}