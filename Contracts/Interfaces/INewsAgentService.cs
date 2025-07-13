using ZinecoMatcher.Contracts.Models;
using ZinecoMatcher.Contracts.Results;

namespace ZinecoMatcher.Contracts.Interfaces
{
    public interface INewsAgentService
    {
        Task<List<ValidationResult>> GetAllChainAgentValidation();

        Task<ValidationResult> GetChainAgentValidation(ZinecoNewsAgent agent);
    }
}
