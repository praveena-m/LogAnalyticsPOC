# LogAnalytics Client for .NET Core
The easiest way to send logs to Azure Log Analytics from your apps.
Construct a custom object and send it to Log Analytics. It will be represented as a log entry in the logs. This helps make logging easy in your applications, and you can focus on more important business logic.

## NuGet
The [LogAnalytics.Client](https://www.nuget.org/packages/loganalytics.client) is available on NuGet.

## How to use the LogAnalytics Client

### Installing the package

#### Install with NuGet Package Manager
```
Install-Package LogAnalytics.Client
```

### Initialize the LogAnalyticsClient

Initialize a new `LogAnalyticsClient` object with a Workspace Id and a Key:
```csharp
LogAnalyticsClient logger = new LogAnalyticsClient(
                workspaceId: "LAW ID",
                sharedKey: "LAW KEY");
```

### Send a single log entry
Synchronous execution (non-HTTP applications):
```csharp
logger.SendLogEntry(new TestEntity
{
    Category = GetCategory(),
    TestString = $"String Test",
    TestBoolean = true,
    TestDateTime = DateTime.UtcNow,
    TestDouble = 2.1,
    TestGuid = Guid.NewGuid()
}, "demolog").Wait();
```

Asynchronous execution (HTTP-based applications):
```csharp
await logger.SendLogEntry(new TestEntity
{
    Category = GetCategory(),
    TestString = $"String Test",
    TestBoolean = true,
    TestDateTime = DateTime.UtcNow,
    TestDouble = 2.1,
    TestGuid = Guid.NewGuid()
}, "demolog")
.ConfigureAwait(false); // Optionally add ConfigureAwait(false) here, depending on your scenario
```

### Send a batch of log entries with one request
If you need to send a lot of log entries at once, it makes better sense to send them as a batch/collection instead of sending them one by one. This saves on requests, resources and eventually costs. 

```csharp
// Example: Wiring up 5000 entities into an "entities" collection.
List<DemoEntity> entities = new List<DemoEntity>();
for (int ii = 0; ii < 5000; ii++)
{
    entities.Add(new DemoEntity
    {
        Criticality = GetCriticality(),
        Message = "lorem ipsum dolor sit amet",
        SystemSource = GetSystemSource()
    });
}

// Send all 5000 log entries at once, in a single request.
await logger.SendLogEntries(entities, "demolog").ConfigureAwait(false);
```

### Typical ASP .NET Core use case
```
IConfiguration Configuration; // ...
services.AddLogAnalyticsClient(Configuration.GetSection("LogAnalytics"));

// manual configuration
services.AddLogAnalyticsClient(c =>
            {
                c.WorkspaceId = "workspaceId";
                c.SharedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("sharredKey"));
            });
```

### Workspace Query and Log Samples
![alt text](https://github.com/praveena-m/LogAnalyticsPOC/blob/main/LogAnalytics.png)


