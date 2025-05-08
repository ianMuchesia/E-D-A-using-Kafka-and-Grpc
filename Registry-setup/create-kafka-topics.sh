#!/bin/bash

# Wait for Kafka to be ready
echo "Waiting for Kafka to be ready..."
sleep 10

# Create topics with proper partitioning and retention
docker exec kafka kafka-topics --create --topic order-created \
    --bootstrap-server kafka:9092 \
    --partitions 3 \
    --replication-factor 1 \
    --config retention.ms=604800000

docker exec kafka kafka-topics --create --topic inventory-reserved \
    --bootstrap-server kafka:9092 \
    --partitions 3 \
    --replication-factor 1 \
    --config retention.ms=604800000
    
docker exec kafka kafka-topics --create --topic out-of-stock \
    --bootstrap-server kafka:9092 \
    --partitions 3 \
    --replication-factor 1 \
    --config retention.ms=604800000
    
docker exec kafka kafka-topics --create --topic payment-processed \
    --bootstrap-server kafka:9092 \
    --partitions 3 \
    --replication-factor 1 \
    --config retention.ms=604800000

# List all topics to verify
docker exec kafka kafka-topics --list --bootstrap-server kafka:9092

echo "Kafka topics created successfully!"