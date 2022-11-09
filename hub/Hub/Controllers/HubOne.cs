using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;
using System.IO;
using SignalRService.Models;
using System.Text.Json;

namespace SignalRService.Controllers
{
    public static class HubOne
    {
        #region Methods

        [FunctionName(nameof(Negotiate))]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "hubone")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName(nameof(Broadcast))]
        public static async Task Broadcast(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalR(HubName = "hubone")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var message = await new StreamReader(req.Body).ReadToEndAsync();
            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "onBroadcastMessage",
                    Arguments = new[] { new BroadcastMessage(message) }
                });
        }

        [FunctionName(nameof(WorkUpdate))]
        public static async Task WorkUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalR(HubName = "hubone")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            var update = await new StreamReader(req.Body).ReadToEndAsync();
            var workUpdateMessage = JsonSerializer.Deserialize<WorkUpdateMessage>(update);

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "onWorkUpdateMessage",
                    Arguments = new[] { workUpdateMessage }
                });
        }

        #endregion
    }
}
