﻿using Microsoft.Extensions.Options;
using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Constants;
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
            var adventureNewsAgents = await _client.GetAsync<NewsAgent>(new Uri(_configuration.Value.AdventureNews.Url));
            foreach (var adventureNewsAgent in adventureNewsAgents)
            {

               if (adventureNewsAgent != null)
                {
                    var distance = GeoDistanceCalculator.CalculateDistanceInMeters(
                        adventureNewsAgent.Latitude, adventureNewsAgent.Longitude,
                        agent.Latitude, agent.Longitude);
                    if (Utils.NormalizeString(adventureNewsAgent.Name) == agent.Name.ToLowerInvariant()
                        && distance <= 100)
                    {
                        return new ValidationResult(true, $"{ValidationMessages.ValidNewsAgentMessage} Adventure News");
                    }
                }
            }
            return new ValidationResult(false, $"{ValidationMessages.InvalidNewsAgentMessage} Adventure News");
        }
    }
}
