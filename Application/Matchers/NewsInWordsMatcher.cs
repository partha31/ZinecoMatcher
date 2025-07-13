using Microsoft.Extensions.Options;
using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;
using ZinecoMatcher.Infrastructure.ApiClient;

namespace ZinecoMatcher.Application.Matchers
{
    public class NewsInWordsMatcher : INewsagentMatcher
    {
        private readonly IApiClient _client;
        private readonly IOptionsSnapshot<ChainApiConfiguration> _configuration;
        public NewsInWordsMatcher(IApiClient client, IOptionsSnapshot<ChainApiConfiguration> configuration) 
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<ValidationResult> ValidateNewsagentAsync(ZinecoNewsAgent agent)
        {
            var nIWAgents = await _client.GetAsync<NewsAgent>(new Uri(_configuration.Value.NewsInWord.Url));

            foreach (var nIWAgent in nIWAgents)
            {
                if (nIWAgent != null)
                {
                    if (Utils.GetReversedName(nIWAgent.Name) == agent.Name)
                    {
                        return new ValidationResult(true, "Match Found");
                    }
                }
            }

            return new ValidationResult(false, "No Match found for given agent");
        }
    }
}
