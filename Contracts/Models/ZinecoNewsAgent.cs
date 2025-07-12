namespace ZinecoMatcher.Contracts.Models
{
    public record ZinecoNewsAgent : NewsAgent
    {
        public required string ChainId { get; init; }
    }
}
