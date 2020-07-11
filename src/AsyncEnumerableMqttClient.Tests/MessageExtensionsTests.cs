using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncEnumerableExtensions.Tests;
using AsyncEnumerableMqttClient.Client;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace AsyncEnumerableMqttClient.Tests
{
	public class MessageExtensionsTests : UnitTestBase
	{
		public MessageExtensionsTests(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public async Task StripStringMessageTest()
		{
			var content = "77";
			var message = new MqttApplicationMessage
			{
				Payload = Encoding.UTF8.GetBytes(content)
			};

			var stream = AsyncEnum.Just(message);

			var result = stream.FromTextValues<int>().Strip();

			var testResult = await result.ToListAsync();

			Assert.True(testResult.First().Content == 77);
		}

		[Fact]
		public async Task StripJsonMessageTest()
		{
			var content = new TestContent()
			{
				Number = 77,
				Text = "Huhu",
				Time = DateTime.Now
			};

			var serializedContent = JsonConvert.SerializeObject(content);

			var message = new MqttApplicationMessage
			{
				Payload = Encoding.UTF8.GetBytes(serializedContent)
			};

			var stream = AsyncEnum.Just(message);

			var result = stream.FromJsonValues<TestContent>().Strip();

			var testResult = await result.ToListAsync();

			Assert.True(Equals(testResult.First().Content, content));
		}

		public class TestContent
		{
			protected bool Equals(TestContent other)
			{
				return Text == other.Text && Time.Equals(other.Time) && Number == other.Number;
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj))
				{
					return false;
				}

				if (ReferenceEquals(this, obj))
				{
					return true;
				}

				if (obj.GetType() != this.GetType())
				{
					return false;
				}

				return Equals((TestContent) obj);
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(Text, Time, Number);
			}

			public string Text { get; set; }
			public DateTime Time { get; set; }

			public int Number { get; set; }

		}
	}
}