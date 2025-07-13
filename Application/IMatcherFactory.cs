using ZinecoMatcher.Contracts.Interfaces;

namespace ZinecoMatcher.Application
{
    public interface IMatcherFactory
    {
        INewsagentMatcher GetAgentMatcher(string chainId);
    }
}
