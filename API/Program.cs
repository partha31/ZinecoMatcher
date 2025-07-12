using Microsoft.AspNetCore.Mvc;
using ZinecoMatcher.Application;
using ZinecoMatcher.Application.Matchers;
using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Infrastructure.ApiClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MatcherFactory>();
//builder.Services.AddScoped<SuperNewsMatcher>();
//builder.Services.AddScoped<NewsInWordsMatcher>();
//builder.Services.AddScoped<AdventureNewsMatcher>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IApiClient, ApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapPost("/getAgentValidation", ([FromBody] ZinecoNewsAgent agent, MatcherFactory factory) => {
    app.Logger.LogInformation($"getAgentValidation API called with context -> " +
        $"{agent.Name}");
    INewsagentMatcher matcher = factory.GetAgentMatcher(agent.ChainId);
    var result = matcher.ValidateNewsagentAsync(agent);
    return result;
}).WithTags("getAgentValidation")
;

app.Run();
