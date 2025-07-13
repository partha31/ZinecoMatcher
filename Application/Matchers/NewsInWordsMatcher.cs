using Microsoft.Extensions.Options;
using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Constants;
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
            var newsInWordsAgents = await _client.GetAsync<NewsAgent>(new Uri(_configuration.Value.NewsInWord.Url));

            foreach (var newsInWordAgent in newsInWordsAgents)
            {
                if (newsInWordAgent != null)
                {
                    if (Utils.GetReversedName(newsInWordAgent.Name) == agent.Name)
                    {
                        return new ValidationResult(true, $"{ValidationMessages.ValidNewsAgentMessage} News In Words");
                    }
                }
            }

            return new ValidationResult(false, $"{ValidationMessages.InvalidNewsAgentMessage} News In Words");
        }
    }
}
