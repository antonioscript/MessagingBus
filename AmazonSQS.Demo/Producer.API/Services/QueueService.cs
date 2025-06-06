using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace Producer.API.Services;

public class QueueService : IQueueService
{
    private readonly IAmazonSQS _sqsClient;

    private const string QueueName = "dinamic-queue"; 

    public QueueService(IAmazonSQS sqsClient)
    {
        _sqsClient = sqsClient;
    }

    public async Task<bool> SendMessageAsync(JsonElement message)
    {
        try
        {
            var queueUrl = await GetOrCreateQueueUrlAsync();

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = message.GetRawText()
            };

            var response = await _sqsClient.SendMessageAsync(sendMessageRequest);

            var success = !string.IsNullOrWhiteSpace(response.MessageId);

            return success;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<string> GetOrCreateQueueUrlAsync()
    {
        try
        {
            var response = await _sqsClient.GetQueueUrlAsync(QueueName);
            return response.QueueUrl;
        }
        catch (QueueDoesNotExistException)
        {
            var response = await _sqsClient.CreateQueueAsync(QueueName);

            return response.QueueUrl;
        }
    }
}
