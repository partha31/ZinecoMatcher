using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ZinecoMatcher.Contracts.Constants;
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
            _logger.LogInformation("Starting validation of all chain agents from Zineco News API.");
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
                        _logger.LogError(ex, "Error validating agent {AgentName} with ChainId {ChainId}", zineCoAgent.Name, zineCoAgent.ChainId);
                        results.Add(new ValidationResult( false, $"{ValidationMessages.InvalidNewsAgentMessage} {zineCoAgent.ChainId}"));
                    }
                }
            }
            return results;
        }

        public async Task<ValidationResult> GetChainAgentValidation(ZinecoNewsAgent agent)
        {
            _logger.LogInformation("Validating started for agent {AgentName} with ChainId {ChainId}", agent.Name, agent.ChainId);
            try
            {
                INewsagentMatcher matcher = _factory.GetAgentMatcher(agent.ChainId);
                var result = await matcher.ValidateNewsagentAsync(agent);
                return result;
            } catch(Exception ex)
            {
                _logger.LogError(ex, "Error validating agent {AgentName} with ChainId {ChainId}", agent.Name, agent.ChainId);
                return new ValidationResult(false, $"{ValidationMessages.InvalidNewsAgentMessage} {agent.ChainId}");
            }
        }
    }
}
