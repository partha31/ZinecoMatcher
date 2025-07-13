using Microsoft.Extensions.Options;
using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Constants;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;
using ZinecoMatcher.Infrastructure.ApiClient;

namespace ZinecoMatcher.Application.Matchers
{
    public class SuperNewsMatcher : INewsagentMatcher
    {
        private readonly IApiClient _client;
        private readonly IOptionsSnapshot<ChainApiConfiguration> _configuration;
        public SuperNewsMatcher(IApiClient client, IOptionsSnapshot<ChainApiConfiguration> configuration) 
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<ValidationResult> ValidateNewsagentAsync(ZinecoNewsAgent agent)
        {
            var superNewsAgents = await _client.GetAsync<NewsAgent>(new Uri(_configuration.Value.SuperNews.Url));

            foreach (var superNewsAgent in superNewsAgents)
            {
                if (superNewsAgent != null)
                {
                    if (Utils.NormalizeString(superNewsAgent.Name) == agent.Name.ToLowerInvariant()
                        && Utils.NormalizeString(Utils.GetFullAddress(superNewsAgent)) == Utils.GetFullAddress(agent).ToLowerInvariant())
                    {
                        return new ValidationResult(true, $"{ValidationMessages.ValidNewsAgentMessage} SuperNews");
                    }
                }
            }

            return new ValidationResult(false, $"{ValidationMessages.InvalidNewsAgentMessage} SuperNews");
        }
    }
}
