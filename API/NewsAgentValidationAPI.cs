﻿using Microsoft.AspNetCore.Mvc;
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
            app.MapGet("/getAllChainAgentValidation", async (INewsAgentService newsAgentservice) => {

                var result = await newsAgentservice.GetAllChainAgentValidation();
                return result;

                }).WithDescription("This API retreive news Agent list for ZineCo from API and return the " +
                "matching validation result for all the agent.");

            app.MapPost("/getChainAgentValidation", ([FromBody] ZinecoNewsAgent agent, INewsAgentService newsAgentservice) =>
            {

                var result = newsAgentservice.GetChainAgentValidation(agent);
                return result;

            });

            return app;
        }
    }
}
