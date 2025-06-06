using Amazon.SQS;
using Amazon.SQS.Model;

namespace Consumer.API;

public class OrderCreatedEventConsumer : BackgroundService
{
    private readonly IAmazonSQS _sqsClient;

    private const string OrderCreatedEventQueueName = "demo-queu";

    public OrderCreatedEventConsumer(IAmazonSQS amazonSQS)
    {
        _sqsClient = amazonSQS;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var queueUrl = await GetQueueUrl(OrderCreatedEventQueueName);

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
                    // Simula processamento de 2 segundos
                    await Task.Delay(2000, stoppingToken);

                    await _sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);
                }
            }
        }
    }

    private async Task<string> GetQueueUrl(string queueName)
    {
        try
        {
            var response = await _sqsClient.GetQueueUrlAsync(queueName);
            return response.QueueUrl;
        }
        catch (QueueDoesNotExistException)
        {
            var response = await _sqsClient.CreateQueueAsync(queueName);
            return response.QueueUrl;
        }
    }
}