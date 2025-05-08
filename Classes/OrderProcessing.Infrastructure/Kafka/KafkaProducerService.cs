using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Confluent.SchemaRegistry.Serdes;

namespace OrderProcessing.Infrastructure.Kafka
{
    public class KafkaProducerService<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly ILogger<KafkaProducerService<TKey, TValue>> _logger;
        private readonly string _topic;

        public KafkaProducerService(
            IOptions<KafkaSettings> settings,
            ILogger<KafkaProducerService<TKey, TValue>> logger,
            ISchemaRegistryClient schemaRegistry,
            string topic)
        {
            _logger = logger;
            _topic = topic;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = settings.Value.BootstrapServers,
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<TKey, TValue>(producerConfig)
                .SetKeySerializer(new Confluent.SchemaRegistry.Serdes.JsonSerializer<TKey>(schemaRegistry))
                .SetValueSerializer(new Confluent.SchemaRegistry.Serdes.JsonSerializer<TValue>(schemaRegistry))
                .SetLogHandler((_, message) =>
                    _logger.LogInformation($"Kafka producer [{message.Name}]: {message.Message}"))
                .SetErrorHandler((_, error) =>
                    _logger.LogError($"Kafka producer error: {error.Reason}"))
                .Build();
        }

        public async Task ProduceAsync(TKey key, TValue message)
        {
            try
            {
                var result = await _producer.ProduceAsync(_topic, new Message<TKey, TValue>
                {
                    Key = key,
                    Value = message
                });

                _logger.LogInformation($"Produced message to {result.Topic}-{result.Partition} with offset {result.Offset}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error producing message");
                throw;
            }
        }

        public void Produce(TKey key, TValue message)
        {
            try
            {
                _producer.Produce(_topic, new Message<TKey, TValue>
                {
                    Key = key,
                    Value = message
                }, deliveryReport =>
                {
                    if (deliveryReport.Error.IsError)
                    {
                        _logger.LogError($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        _logger.LogInformation($"Produced message to {deliveryReport.Topic}-{deliveryReport.Partition} with offset {deliveryReport.Offset}");
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error producing message");
                throw;
            }
        }

        public void Dispose()
        {
            _producer?.Flush();
            _producer?.Dispose();
        }
    }
}