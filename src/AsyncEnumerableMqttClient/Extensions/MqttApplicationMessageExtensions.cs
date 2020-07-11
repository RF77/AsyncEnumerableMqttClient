// /*******************************************************************************
//  * Copyright (c) 2020 by RF77 (https://github.com/RF77)
//  * All rights reserved. This program and the accompanying materials
//  * are made available under the terms of the Eclipse Public License v1.0
//  * which accompanies this distribution, and is available at
//  * http://www.eclipse.org/legal/epl-v10.html
//  *
//  * Contributors:
//  *    RF77 - initial API and implementation and/or initial documentation
//  *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AsyncEnumerableMqttClient.Client;
using MQTTnet.Diagnostics;

namespace MQTTnet.Extensions.ManagedClient
{
	public static class MqttApplicationMessageExtensions
	{
		/// <summary>
		/// Deserilaize the MQTT message payload to type T assuming the payload is valid serialized JSON
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IAsyncEnumerable<JsonMessage<T>> FromJsonValues<T>(this IAsyncEnumerable<MqttApplicationMessage> source)
		{
			return source.Select(i => new JsonMessage<T>(i));
		}

		/// <summary>
		/// Deserilaize the MQTT message payload to type T assuming the payload is valid serialized string, e.g. "5" to int => 5
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IAsyncEnumerable<TextMessage<T>> FromTextValues<T>(this IAsyncEnumerable<MqttApplicationMessage> source)
		{
			return source.Select(i => new TextMessage<T>(i));
		}

		/// <summary>
		/// Assuming the payload is a plain byte[]
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IAsyncEnumerable<ByteArrayMessage> FromByteValues(this IAsyncEnumerable<MqttApplicationMessage> source)
		{
			return source.Select(i => new ByteArrayMessage(i));
		}

		/// <summary>
		/// Filter by one specified Topic
		/// </summary>
		/// <param name="source"></param>
		/// <param name="topic"></param>
		/// <returns>Filtered stream</returns>
		public static IAsyncEnumerable<MqttApplicationMessage> WhereTopic(this IAsyncEnumerable<MqttApplicationMessage> source, string topic)
		{
			return source.Where(i => i.Topic == topic);
		}

		/// <summary>
		/// Filter Topics starting with the name
		/// </summary>
		/// <param name="source"></param>
		/// <param name="topic"></param>
		/// <returns>Filtered stream</returns>
		public static IAsyncEnumerable<MqttApplicationMessage> WhereTopicStartsWith(this IAsyncEnumerable<MqttApplicationMessage> source, string topic)
		{
			return source.Where(i => i.Topic.StartsWith(topic));
		}

		/// <summary>
		/// Filter Topics matching the regex
		/// </summary>
		/// <param name="source"></param>
		/// <param name="topicRegex"></param>
		/// <returns>Filtered stream</returns>
		public static IAsyncEnumerable<MqttApplicationMessage> WhereTopicRegex(this IAsyncEnumerable<MqttApplicationMessage> source, string topicRegex)
		{
			return source.Where(i => Regex.IsMatch(i.Topic, topicRegex, RegexOptions.Compiled));
		}
	}
}