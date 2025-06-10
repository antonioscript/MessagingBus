using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

namespace Consumer.API.Data;
public class MongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<BsonDocument> QueueCollection => _database.GetCollection<BsonDocument>("QueueCollection");
}