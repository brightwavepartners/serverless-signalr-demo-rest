using Api.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;
using System.Text;
using System.Text.Json;

namespace Api.Services
{
    public class SignalRService
    {
        #region Fields

        private readonly string _baseUrl = "http://localhost:7159/api/"; // points to azure function app "hub"

        #endregion

        #region Methods

        public async Task SendWorkUpdateMessage(int value)
        {
            var workUpdateMessage = new WorkUpdateMessage(value);

            var json = JsonSerializer.Serialize(workUpdateMessage);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{_baseUrl}workupdate";

            using var client = new HttpClient();
            await client.PostAsync(url, data);
        }

        #endregion
    }
}
