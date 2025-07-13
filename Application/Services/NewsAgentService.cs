using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;
using ZinecoMatcher.Infrastructure.ApiClient;

namespace ZinecoMatcher.Application.Services
{
    public class NewsAgentService : INewsAgentService
    {
        private readonly IApiClient _client;
        private readonly IOptionsSnapshot<ChainApiConfiguration> _configuration;
        private readonly ILogger<NewsAgentService> _logger;
        private readonly IMatcherFactory _factory;
        public NewsAgentService(IApiClient client, IOptionsSnapshot<ChainApiConfiguration> configuration, 
            ILogger<NewsAgentService> logger, IMatcherFactory factory)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
            _factory = factory;
        }

        public async Task<List<ValidationResult>> GetAllChainAgentValidation()
        {
            List<ValidationResult> results = new List<ValidationResult>();
            var zineCoAgents = await _client.GetAsync<ZinecoNewsAgent>(new Uri(_configuration.Value.ZineCoNews.Url));
            foreach (var zineCoAgent in zineCoAgents)
            {
                if (zineCoAgent != null && zineCoAgent.ChainId != null)
                {
                    try
                    {
                        INewsagentMatcher matcher = _factory.GetAgentMatcher(zineCoAgent.ChainId);
                        results.Add(await matcher.ValidateNewsagentAsync(zineCoAgent));
                    }
                    catch (Exception ex) 
                    {
                        results.Add(new ValidationResult( false, ex.Message));
                    }
                }
            }
            return results;
        }

        public async Task<ValidationResult> GetChainAgentValidation(ZinecoNewsAgent agent)
        {
            try
            {
                INewsagentMatcher matcher = _factory.GetAgentMatcher(agent.ChainId);
                var result = await matcher.ValidateNewsagentAsync(agent);
                return result;
            } catch(Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
        }
    }
}
