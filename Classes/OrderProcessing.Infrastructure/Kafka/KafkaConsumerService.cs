using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.Kafka.SyncOverAsync;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;


namespace OrderProcessing.Infrastructure.Kafka
{
    public class KafkaConsumerService<TKey, TValue> : BackgroundService
        where TKey : class
        where TValue : class
    {
        private readonly IConsumer<TKey, TValue> _consumer;
        private readonly ILogger<KafkaConsumerService<TKey, TValue>> _logger;
        private readonly string _topic;
        private readonly Func<TKey, TValue, Task> _messageHandler;

        public KafkaConsumerService(
            IOptions<KafkaSettings> settings,
            ILogger<KafkaConsumerService<TKey, TValue>> logger,
            ISchemaRegistryClient schemaRegistry,
            string topic,
            string consumerGroup,
            Func<TKey, TValue, Task> messageHandler)
        {
            _logger = logger;
            _topic = topic;
            _messageHandler = messageHandler;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = settings.Value.BootstrapServers,
                GroupId = $"{consumerGroup}-consumer",
                EnableAutoCommit = settings.Value.EnableAutoCommit,
                AutoOffsetReset = (AutoOffsetReset)settings.Value.AutoOffsetReset,
                SessionTimeoutMs = settings.Value.SessionTimeoutMs
            };

            var keyDeserializer = new Confluent.SchemaRegistry.Serdes.JsonDeserializer<TKey>().AsSyncOverAsync();
            var valueDeserializer = new Confluent.SchemaRegistry.Serdes.JsonDeserializer<TValue>().AsSyncOverAsync();
            
            _consumer = new ConsumerBuilder<TKey, TValue>(consumerConfig)
                .SetKeyDeserializer(keyDeserializer)
                .SetValueDeserializer(valueDeserializer)
                .SetLogHandler((_, message) =>
                    _logger.LogInformation($"Kafka consumer [{message.Name}]: {message.Message}"))
                .SetErrorHandler((_, error) =>
                    _logger.LogError($"Kafka consumer error: {error.Reason}"))
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topic);
            _logger.LogInformation($"Started consuming from topic {_topic}");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result != null)
                    {
                        _logger.LogInformation($"Consumed message from {result.Topic}-{result.Partition} with offset {result.Offset}");

                        await _messageHandler(result.Message.Key, result.Message.Value);

                        if (!stoppingToken.IsCancellationRequested)
                        {
                            _consumer.Commit(result);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming message");
                }
            }
        }

        public override void Dispose()
        {
            _consumer?.Close();
            _consumer?.Dispose();
            base.Dispose();
        }
    }
}