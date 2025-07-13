using ZinecoMatcher.Application.Matchers;
using ZinecoMatcher.Contracts.Interfaces;

namespace ZinecoMatcher.Application
{
    public class MatcherFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MatcherFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INewsagentMatcher GetAgentMatcher(string chainId)
        {
            var scope = _serviceProvider.CreateScope();
            switch (chainId)
            {
                case "SUP": return scope.ServiceProvider.GetRequiredService<SuperNewsMatcher>();
                case "ADV": return scope.ServiceProvider.GetRequiredService<AdventureNewsMatcher>();
                case "NIW": return scope.ServiceProvider.GetRequiredService<NewsInWordsMatcher>();
                default: throw new Exception($"Chain for {chainId} is not valid");
            }
        }
    }
}
