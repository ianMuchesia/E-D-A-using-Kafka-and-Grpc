using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.SchemaRegistry;
using Microsoft.Extensions.Options;

namespace OrderProcessing.Infrastructure.Kafka
{
    public class SchemaRegistryFactory
    {
        private readonly KafkaSettings _settings;

        public SchemaRegistryFactory(IOptions<KafkaSettings> settings)
        {
            _settings = settings.Value;
        }

        public ISchemaRegistryClient CreateSchemaRegistryClient()
        {
            return new CachedSchemaRegistryClient(new SchemaRegistryConfig
            {
                Url = _settings.SchemaRegistryUrl
            });
        }
    }
}
