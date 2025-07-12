using ZinecoMatcher.Contracts.Interfaces;

namespace ZinecoMatcher.Application
{
    public interface IMatchFactory
    {
        INewsagentMatcher GetAgentMatcher(string chainId);
    }
}
