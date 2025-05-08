# gRPC + Kafka Event-Driven Architecture (EDA)

This project demonstrates an **Event-Driven Architecture (EDA)** using **gRPC** for communication between services and **Kafka** for event streaming. The system is designed to handle a variety of event-driven use cases with Kafka as the backbone of event flow and gRPC for service-to-service communication.

## Project Structure

```
gRPC-Kafka-EDA/
├── src/
│   ├── gRPC-Kafka-EDA.Domain/                 # Shared domain models
│   ├── gRPC-Kafka-EDA.Events/                 # Avro event schemas and generated classes
│   ├── gRPC-Kafka-EDA.Infrastructure/         # Shared infrastructure components
│   │   ├── Kafka/                              # Kafka configuration and helpers
│   │   └── Grpc/                               # gRPC shared code
│   ├── gRPC-Kafka-EDA.Service1/               # Service 1 implementation
│   │   ├── Domain/                             # Domain models
│   │   ├── Application/                        # Application services
│   │   ├── Infrastructure/                     # Infrastructure implementations
│   │   ├── API/                                # Web API controllers
│   │   └── Program.cs                          # Entry point
│   ├── gRPC-Kafka-EDA.Service2/               # Service 2 implementation
│   │   ├── Domain/                             # Domain models
│   │   ├── Application/                        # Application services
│   │   ├── Infrastructure/                     # Infrastructure implementations
│   │   ├── API/                                # Web API controllers
│   │   └── Program.cs                          # Entry point
│   └── gRPC-Kafka-EDA.Service3/               # Service 3 implementation
│       ├── Domain/                             # Domain models
│       ├── Application/                        # Application services
│       ├── Infrastructure/                     # Infrastructure implementations
│       ├── API/                                # Web API controllers
│       └── Program.cs                          # Entry point
├── tests/                                      # Test projects
│   ├── gRPC-Kafka-EDA.Service1.Tests/
│   ├── gRPC-Kafka-EDA.Service2.Tests/
│   └── gRPC-Kafka-EDA.Service3.Tests/
├── docker/                                     # Docker related files
│   ├── docker-compose.yml                      # Kafka, ZooKeeper, Schema Registry setup
│   └── create-kafka-topics.sh                  # Kafka topics creation script
└── gRPC-Kafka-EDA.sln                         # Solution file
```

## Prerequisites

- Visual Studio 2022 or later
- Docker (for running Kafka, ZooKeeper, and Schema Registry)
- .NET 8.0 SDK
- Docker Compose

## Setting Up the Project

### Step 1: Clone the Repository

```bash
git clone https://github.com/yourusername/grpc-kafka-eda.git
cd grpc-kafka-eda
```

### Step 2: Build the Solution

Open the solution in Visual Studio:

```bash
dotnet open gRPC-Kafka-EDA.sln
```

### Step 3: Install Dependencies

Each service has its own dependencies. You can restore them by running:

```bash
dotnet restore
```

### Step 4: Set Up Avro Code Generation

For the **Events** project:

1. Open the `gRPC-Kafka-EDA.Events.csproj` file.
2. Add the following Avro code generation configuration:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Apache.Avro" Version="1.11.3" />
    <PackageReference Include="Confluent.SchemaRegistry.Serdes.Avro" Version="2.3.0" />
  </ItemGroup>

  <ItemGroup>
    <Avro Include="Schemas\*.avsc" />
  </ItemGroup>

  <Target Name="PreBuild" BeforeTargets="PreBuildEvent">
    <Exec Command="dotnet avrogen -s Schemas/*.avsc ." />
  </Target>

</Project>
```

This will automatically generate C# classes from Avro schemas for Kafka event handling.

### Step 5: Configure Kafka and gRPC in Docker

1. Navigate to the **docker** directory.
2. Open the `docker-compose.yml` file to configure Kafka, ZooKeeper, and Schema Registry.
3. Run the following command to start Kafka, ZooKeeper, and Schema Registry:

```bash
docker-compose up -d
```

4. After containers are up, run the Kafka topic creation script:

```bash
./create-kafka-topics.sh
```

### Step 6: Configure the gRPC Services

For each service:

1. Add the necessary NuGet packages in the `.csproj` files:

```xml
<ItemGroup>
  <PackageReference Include="Confluent.Kafka" Version="2.3.0" />
  <PackageReference Include="Confluent.SchemaRegistry" Version="2.3.0" />
  <PackageReference Include="Confluent.SchemaRegistry.Serdes.Avro" Version="2.3.0" />
  <PackageReference Include="Grpc.AspNetCore" Version="2.59.0" />
  <PackageReference Include="Grpc.AspNetCore.Server.Reflection" Version="2.59.0" />
  <PackageReference Include="Grpc.Net.Client" Version="2.59.0" />
  <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
  <PackageReference Include="Microsoft.Extensions.Http" Version="8.0.0" />
</ItemGroup>
```

2. Add the Protobuf references to enable gRPC communication:

```xml
<ItemGroup>
  <Protobuf Include="Protos\*.proto" GrpcServices="Both" />
</ItemGroup>
```

3. Ensure `.proto` files are added to the `Protos` directory.

### Step 7: Set Up Multiple Service Startup

In Visual Studio:

1. Right-click on the solution → **Properties**.
2. Under **Startup Project**, select **Multiple startup projects**.
3. Set all services (e.g., `gRPC-Kafka-EDA.Service1`, `gRPC-Kafka-EDA.Service2`, `gRPC-Kafka-EDA.Service3`) to **Start**.

### Step 8: Run the Solution

To run the services:

1. Press **F5** in Visual Studio to start all services simultaneously.
2. Kafka and the services should now be running and able to communicate via events.

## Testing the System

Use tools like **Postman** to test the system by sending requests to each service's API. Verify the flow of events through Kafka and ensure that all services are processing events correctly.

## Troubleshooting

* **Kafka Connection Issues**: Ensure Docker containers are running and Kafka bootstrap servers are configured correctly in each service.
* **gRPC Communication Issues**: Verify server addresses are correct and ports are not blocked.
* **Service Startup Order**: Services should be resilient to Kafka being temporarily unavailable during startup. Ensure appropriate error handling and retry mechanisms are in place.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.