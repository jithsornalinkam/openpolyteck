using JsonConverterApp.Core.Interfaces;
using System.Text.Json;
using System.Threading;
using System;

namespace JsonConverterApp.Infrastructure
{
    public class ApiHandler : IReadDataHandler
    {

        private readonly HttpClient _httpClient;

        public ApiHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> ReadFileAsync(string path)
        {
            var response =  await _httpClient.GetAsync(path);
            var contents = await response.Content.ReadAsStringAsync();
            var cleanedupString = contents.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
            return cleanedupString;
        }
    }
}
