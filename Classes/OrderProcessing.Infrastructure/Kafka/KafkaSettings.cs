using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Infrastructure.Kafka
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "localhost:29092";
        public string SchemaRegistryUrl { get; set; } = "http://localhost:8081";
        public string ClientId { get; set; } = "order-processing";
        public string GroupId { get; set; } = "order-processing-group";
        public bool EnableAutoCommit { get; set; } = false;
        public int SessionTimeoutMs { get; set; } = 30000;
        public int AutoOffsetReset { get; set; } = 1; // Earliest
    }
}
