using Amazon.SQS;
using Amazon.SQS.Model;

namespace Consumer.API;

public class OrderCreatedEventConsumer : BackgroundService
{
    private readonly ILogger<OrderCreatedEventConsumer> _logger;
    private readonly IAmazonSQS _sqsClient;
    private const string OrderCreatedEventQueueName = "demo-queu";

    public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger, IAmazonSQS amazonSQS)
    {
        _logger = logger;
        _sqsClient = amazonSQS;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Polling Queue {queueName}", OrderCreatedEventQueueName);

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
                    _logger.LogInformation("Received Message from Queue {queueName} with body as:\n{body}", OrderCreatedEventQueueName, message.Body);

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
            _logger.LogInformation("Queue {queueName} doesn't exist. Creating...", queueName);
            var response = await _sqsClient.CreateQueueAsync(queueName);
            return response.QueueUrl;
        }
    }
}