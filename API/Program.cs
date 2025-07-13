using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using ZinecoMatcher.API;
using ZinecoMatcher.Application;
using ZinecoMatcher.Application.Matchers;
using ZinecoMatcher.Application.Services;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Infrastructure.ApiClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMatcherFactory, MatcherFactory>();
builder.Services.AddScoped<SuperNewsMatcher>();
builder.Services.AddScoped<NewsInWordsMatcher>();
builder.Services.AddScoped<AdventureNewsMatcher>();

builder.Services.AddScoped<INewsAgentService, NewsAgentService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure api client with resilience handlers for API clients
builder.Services.AddHttpClient<IApiClient, ApiClient>()
    .AddResilienceHandler("ChainApiOption", builder =>
    {
        builder.AddRetry(new HttpRetryStrategyOptions
        {
            BackoffType = DelayBackoffType.Exponential,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(3),
        });
        builder.AddTimeout(TimeSpan.FromSeconds(10));
        builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
        {
            FailureRatio = 0.5,
            SamplingDuration = TimeSpan.FromSeconds(30),
            MinimumThroughput = 10,
            BreakDuration = TimeSpan.FromSeconds(15)
        });
     });

// configure chain api configurations with defined models

builder.Services.Configure<ChainApiConfiguration>(
    builder.Configuration.GetSection("AgentChainApis")
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Added minimal API for both get all and single validation result.
app.MapNewsAgentValidationEndpoints();

app.Run();
