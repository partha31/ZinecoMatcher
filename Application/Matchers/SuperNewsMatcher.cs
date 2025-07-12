using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;
using ZinecoMatcher.Infrastructure.ApiClient;

namespace ZinecoMatcher.Application.Matchers
{
    public class SuperNewsMatcher : INewsagentMatcher
    {
        private readonly IApiClient _client;
        public SuperNewsMatcher(IApiClient client) 
        {
            _client = client;
        }
        public async Task<ValidationResult> ValidateNewsagentAsync(ZinecoNewsAgent agent)
        {
            var superNewsAgents = await _client.GetAsync<NewsAgent>("https://jhuzpclbs4.execute-api.ap-southeast2.amazonaws.com/prod/locations");

            foreach (var superNewsAgent in superNewsAgents)
            {
                if (superNewsAgent != null)
                {
                    if (Utils.NormalizeString(superNewsAgent.Name) == agent.Name.ToLowerInvariant()
                        && Utils.NormalizeString(Utils.GetFullAddress(superNewsAgent)) == Utils.GetFullAddress(agent))
                    {
                        return new ValidationResult(true, "Match Found");
                    }
                }
            }

            return new ValidationResult(false, "No Match found for given agent");
        }
    }
}
