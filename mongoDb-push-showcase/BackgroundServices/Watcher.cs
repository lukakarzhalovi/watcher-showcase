using mongoDb_push_showcase.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mongoDb_push_showcase.BackgroundServices;

public class Watcher(IMongoDatabase mongoDatabase) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var pipeline = new BsonDocument[]
        {
            new("$match", new BsonDocument
            {
                { "operationType", "update" },
                { "updateDescription.updatedFields.DateUpdated", new BsonDocument("$exists", true) }
            })
        };

    var collection = mongoDatabase.GetCollection<Transaction>("Transactions");

    using var cursor = await collection.WatchAsync<ChangeStreamDocument<Transaction>>(
        pipeline
            .Select(p => (PipelineStageDefinition<ChangeStreamDocument<Transaction>, ChangeStreamDocument<Transaction>>)p)
            .ToArray(),
        cancellationToken: stoppingToken);
    
    
    await cursor.ForEachAsync(change =>
    {
        Console.WriteLine("Received the following change: " + change);
    }, cancellationToken: stoppingToken);
    }
}