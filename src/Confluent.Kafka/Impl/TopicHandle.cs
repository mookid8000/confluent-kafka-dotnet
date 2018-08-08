// Copyright 2016-2017 Confluent Inc., 2015-2016 Andreas Heider
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Derived from: rdkafka-dotnet, licensed under the 2-clause BSD License.
//
// Refer to LICENSE for more information.

using System;
using System.Runtime.InteropServices;
using Confluent.Kafka.Internal;


namespace Confluent.Kafka.Impl
{
    enum MsgFlags
    {
        MSG_F_FREE = 1,
        MSG_F_COPY = 2,
        MSG_F_BLOCK = 4
    }

    /// <remarks>
    ///     TODO: support finalization.
    /// </remarks>
    internal sealed class TopicHandle : IDisposable
    {
        const int RD_KAFKA_PARTITION_UA = -1;

        private IntPtr handle;
        internal KafkaHandle kafkaHandle;

        public TopicHandle(IntPtr handle) { this.handle = handle; }

        public void Dispose()
        {
            Librdkafka.topic_destroy(this.handle);
        }

        internal string GetName()
            => Util.Marshal.PtrToStringUTF8(Librdkafka.topic_name(handle));

        internal bool PartitionAvailable(int partition)
        {
            return Librdkafka.topic_partition_available(handle, partition);
        }

        internal IntPtr Handle => handle;
    }
}