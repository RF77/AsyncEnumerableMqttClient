// Copyright (c) 2020 by RF77 (https://github.com/RF77)
// Licensed under the Apache 2.0 License.
// See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;
using MQTTnet.Extensions.ManagedClient;
using Nito.AsyncEx;
using Xunit;
using Xunit.Abstractions;

namespace AsyncEnumerableMqttClient.Tests
{
	public class MqttStreamTests : MqttTests
	{
		public MqttStreamTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public void TestMqttWithStringContent()
		{
			AsyncContext.Run(async () =>
			{
				await MqttClient.StartAsync(MqttOptions);
				await MqttClient.WaitUntilConnectedAsync();

				var topic = GetTopic("MyStringContent");
				await MqttClient.SubscribeAsync(topic);
				var myNumbers = new[] {4, 8, 15, 16, 23, 42};

				var stream = MqttClient.ReceivedMessages.WhereTopic(topic).FromTextValues<int>().RemoveTimeStamp();
				var task = stream.ToListAsync();

				foreach (var myNumber in myNumbers)
				{
					var pubResult = await MqttClient.PublishAsync(m => m.WithTopic(topic).WithPayload(myNumber.ToString()));
				}

				// time to receive messages
				await Task.Delay(500);

				await MqttClient.StopAsync();

				var result = await task;

				Assert.True(myNumbers.SequenceEqual(result));
			});
		}
	}
}