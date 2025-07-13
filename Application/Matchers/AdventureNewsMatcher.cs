using Microsoft.Extensions.Options;
using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;
using ZinecoMatcher.Infrastructure.ApiClient;

namespace ZinecoMatcher.Application.Matchers
{
    public class AdventureNewsMatcher : INewsagentMatcher
    {
        private readonly IApiClient _client;
        private readonly IOptionsSnapshot<ChainApiConfiguration> _configuration;
        public AdventureNewsMatcher(IApiClient client, IOptionsSnapshot<ChainApiConfiguration> configuration) 
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<ValidationResult> ValidateNewsagentAsync(ZinecoNewsAgent agent)
        {
            var aDVAgents = await _client.GetAsync<NewsAgent>(new Uri(_configuration.Value.AdventureNews.Url));
            foreach (var aDVAgent in aDVAgents)
            {

               if (aDVAgent != null)
                {
                    var distance = GeoDistanceCalculator.CalculateDistanceInMeters(
                        aDVAgent.Latitude, aDVAgent.Longitude,
                        agent.Latitude, agent.Longitude);
                    if (Utils.NormalizeString(aDVAgent.Name) == agent.Name.ToLowerInvariant()
                        && distance <= 100)
                    {
                        return new ValidationResult(true, "Match Found");
                    }
                }
            }
            return new ValidationResult(false, "No Match found for given agent");
        }
    }
}
