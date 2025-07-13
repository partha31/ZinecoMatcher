using System.Text.Json;

namespace ZinecoMatcher.Infrastructure.ApiClient
{
    public class ApiClient: IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;
        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<T>> GetAsync<T>(Uri path)
        {
            try
            {
                var response = await _httpClient.GetAsync(path);
                var agentList = new List<T>();

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    agentList = JsonSerializer.Deserialize<List<T>>(jsonResponse,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                    return agentList ?? new List<T>();
                }
                return agentList;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error in third party api call");
                return new List<T>();
            }
        }
    }
}
