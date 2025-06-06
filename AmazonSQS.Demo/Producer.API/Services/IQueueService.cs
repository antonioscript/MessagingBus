using System.Text.Json;
using System.Threading.Tasks;

namespace Producer.API.Services;

public interface IQueueService
{
    Task<bool> SendMessageAsync(JsonElement message);
}
