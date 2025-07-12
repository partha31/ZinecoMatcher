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
            switch(chainId)
            {
                case "SUP": return _serviceProvider.GetRequiredService<SuperNewsMatcher>();
                case "ADV": return _serviceProvider.GetRequiredService<AdventureNewsMatcher>();
                case "NIW": return _serviceProvider.GetRequiredService<NewsInWordsMatcher>();
                default: throw new Exception($"Chain for {chainId} is not valid");
            }
        }
    }
}
