// Copyright (c) 2020 by RF77 (https://github.com/RF77)
// Licensed under the Apache 2.0 License.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using AsyncEnumerableExtensions.TimeOperators;
using AsyncEnumerableMqttClient.Client;

namespace MQTTnet.Extensions.ManagedClient
{
	public static class MessageExtension
	{
		/// <summary>
		/// In case you don't need the mqtt message overhead, you can strip it
		/// </summary>
		/// <typeparam name="TContent">Type of string item</typeparam>
		/// <param name="source">TextMessage stream</param>
		/// <returns>Plain TimeStampItem</returns>
		public static IAsyncEnumerable<ITimeStampItem<TContent>> Strip<TContent>(this IAsyncEnumerable<TextMessage<TContent>> source)
		{
			return source.Select(i => new TimeStampItem<TContent>(i.TimeStamp, i.Content)).OfType<ITimeStampItem<TContent>>();
		}

		/// <summary>
		/// In case you don't need the mqtt message overhead, you can strip it
		/// </summary>
		/// <typeparam name="TContent">Type of string item</typeparam>
		/// <param name="source">JsonMessage stream</param>
		/// <returns>Plain TimeStampItem</returns>
		public static IAsyncEnumerable<ITimeStampItem<TContent>> Strip<TContent>(this IAsyncEnumerable<JsonMessage<TContent>> source)
		{
			return source.Select(i => new TimeStampItem<TContent>(i.TimeStamp, i.Content)).OfType<ITimeStampItem<TContent>>();
		}

		/// <summary>
		/// In case you don't need the mqtt message overhead, you can strip it
		/// </summary>
		/// <param name="source">ByteArrayMessage stream</param>
		/// <returns>Plain TimeStampItem</returns>
		public static IAsyncEnumerable<ITimeStampItem<byte[]>> Strip(this IAsyncEnumerable<ByteArrayMessage> source)
		{
			return source.Select(i => new TimeStampItem<byte[]>(i.TimeStamp, i.Content)).OfType<ITimeStampItem<byte[]>>();
		}
	}
}