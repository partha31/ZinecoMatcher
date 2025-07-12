using ZinecoMatcher.Contracts.Interfaces;
using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;

namespace ZinecoMatcher.Application.Matchers
{
    public class AdventureNewsMatcher : INewsagentMatcher
    {
        public Task<ValidationResult> ValidateNewsagentAsync(ZinecoNewsAgent agent)
        {
            throw new NotImplementedException();
        }
    }
}
