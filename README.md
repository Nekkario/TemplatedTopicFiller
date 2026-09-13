# TemplatedTopicFiller

A .NET console worker that generates messages from a configurable template and publishes them to a Kafka topic. Runs a fixed number of times and exits.

## How it works

On startup, the service reads `Count` from configuration and, for each iteration, renders `KeyTemplate`/`ValueTemplate` by replacing tokens with random values, then publishes the resulting key/value pair to the configured Kafka topic. Once `Count` messages are published, the process stops itself.

### Template tokens

A template is a string containing one or more tokens. Each token is replaced with a randomly generated value:

| Token       | Value                          | Range syntax             |
|-------------|---------------------------------|--------------------------|
| `int`       | Random integer                  | `int(min, max)`          |
| `long`      | Random long                      | `long(min, max)`         |
| `double`    | Random double, 3 decimal places  | `double(min, max)`       |
| `string`    | Random lowercase string          | not supported            |
| `bool`      | `true` or `false`                | not supported            |
| `date`      | Date within the next 30 days     | not supported            |

If a range is omitted, a built-in default range is used. Any text outside of recognized tokens is left as-is, so a template can be a full JSON body:

```json
{
	"user_id": int(1, 100000),
	"name": "string",
	"is_active": bool,
	"balance": double(0, 1000),
	"registered_at": "date"
}
```

## Configuration

Settings live in `Service/appsettings.json` and can be overridden with environment variables.

```json
{
  "MessageTemplate": {
    "KeyTemplate": "long",
    "ValueTemplate": "..."
  },
  "MessageSettings": {
    "Count": 10
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "TopicName": "test-topic",
    "SecurityProtocol": "PLAINTEXT",
    "ConsumerGroupId": "my-csharp-consumer"
  }
}
```

| Section          | Key                | Description                                  |
|------------------|---------------------|-----------------------------------------------|
| `MessageTemplate` | `KeyTemplate`       | Template used to render the message key       |
| `MessageTemplate` | `ValueTemplate`     | Template used to render the message value      |
| `MessageSettings` | `Count`             | Number of messages to generate and publish     |
| `Kafka`           | `BootstrapServers`  | Kafka broker addresses                         |
| `Kafka`           | `TopicName`         | Target Kafka topic                             |
| `Kafka`           | `SecurityProtocol`  | Kafka security protocol (e.g. `PLAINTEXT`)     |
| `Kafka`           | `ConsumerGroupId`   | Consumer group identifier                      |

## Running

Requires the .NET 10 SDK and a reachable Kafka broker.

```bash
dotnet run --project Service
```

## Project structure

| Project             | Responsibility                                                        |
|----------------------|------------------------------------------------------------------------|
| `Abstractions`       | Interfaces shared across layers (`Logic`, `Transport`)                |
| `Models.Domain`      | Domain model (`GeneratedMessage`)                                     |
| `Models.Transport`   | Configuration option classes                                          |
| `Logic`              | Template rendering and token generation strategies                    |
| `Transport`          | Kafka publishing and message serialization                            |
| `Service`            | Host process wiring and the background service entry point            |
