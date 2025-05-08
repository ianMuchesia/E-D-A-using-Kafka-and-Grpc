using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Infrastructure.Kafka;

namespace OrderProcessing.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaSettings>(configuration.GetSection("Kafka"));

            services.AddSingleton<SchemaRegistryFactory>();
            services.AddSingleton(sp => sp.GetRequiredService<SchemaRegistryFactory>().CreateSchemaRegistryClient());

            return services;
        }
    }
}
