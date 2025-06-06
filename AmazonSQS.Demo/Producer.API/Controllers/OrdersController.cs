using Amazon.SQS.Model;
using Amazon.SQS;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Producer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IAmazonSQS _sqsClient;
    private const string OrderCreatedEventQueueName = "dinamic-queue";

    public OrdersController(IAmazonSQS sqsClient)
    {
        _sqsClient = sqsClient;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] JsonElement jsonElement)
    {
        try
        {
            if (!jsonElement.TryGetProperty("accountId", out var _) ||
                !jsonElement.TryGetProperty("personType", out var _))
            {
                return BadRequest(new { message = "Campos obrigatórios 'accountId' ou 'personType' estão ausentes." });
            }

            var queueUrl = await GetQueueUrl(OrderCreatedEventQueueName);

            var messageBody = jsonElement.GetRawText();

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = messageBody
            };

            var result = await _sqsClient.SendMessageAsync(sendMessageRequest);

            return Ok(new { message = "Mensagem enviada com sucesso!", sqsMessageId = result.MessageId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao processar a requisição.", details = ex.Message });
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
