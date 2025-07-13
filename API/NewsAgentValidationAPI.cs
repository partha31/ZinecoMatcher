using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZinecoMatcher.Application;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;
using ZinecoMatcher.Infrastructure.ApiClient;

namespace ZinecoMatcher.API
{
    public static class NewsAgentValidationAPI
    {
        public static IEndpointRouteBuilder MapNewsAgentValidationEndpoints(this IEndpointRouteBuilder app)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            app.MapPost("/getAgentValidation", async ( MatcherFactory factory,
                ILogger logger, ApiClient client, IOptionsSnapshot<ChainApiConfiguration> configuration) => {
                    
                    var zineCoAgents = await client.GetAsync<ZinecoNewsAgent>(new Uri(configuration.Value.ZineCoNews.Url));
                    foreach (var zineCoAgent in zineCoAgents)
                    {
                        if (zineCoAgent != null && zineCoAgent.ChainId != null)
                        {
                            INewsagentMatcher matcher = factory.GetAgentMatcher(zineCoAgent.ChainId);
                            var result = await matcher.ValidateNewsagentAsync(zineCoAgent);
                            results.Add(result);
                        }
                    }

                    return results;

                }).WithTags("getAgentValidation");

            return app;
        }
    }
}
