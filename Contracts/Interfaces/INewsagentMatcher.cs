using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;

namespace ZinecoMatcher.Contracts.Interfaces
{
    public interface INewsagentMatcher
    {
        Task<ValidationResult> ValidateNewsagentAsync(ZinecoNewsAgent agent);
    }
}
