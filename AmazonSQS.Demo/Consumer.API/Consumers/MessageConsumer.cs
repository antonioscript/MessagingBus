using Amazon.SQS;
using Amazon.SQS.Model;
using Consumer.API.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Producer.API.Consumers;

public class MessageConsumer : BackgroundService 
{
    private readonly IAmazonSQS _sqsClient;
    private readonly IMongoCollection<BsonDocument> _collection;

    private const string QueueName = "dinamic-queue";

    public MessageConsumer(IAmazonSQS sqsClient, MongoDBContext context)
    {
        _sqsClient = sqsClient;
        _collection = context.QueueCollection;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = (await _sqsClient.GetQueueUrlAsync(QueueName)).QueueUrl;

        var receiveRequest = new ReceiveMessageRequest()
        {
            QueueUrl = queueUrl
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _sqsClient.ReceiveMessageAsync(receiveRequest);

            if (response.Messages?.Any() == true)
            {
                foreach (var message in response.Messages)
                {

                    var document = BsonSerializer.Deserialize<BsonDocument>(message.Body);
                    await _collection.InsertOneAsync(document, cancellationToken: stoppingToken);

                    await _sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
                }
            }
        }
    }
}
