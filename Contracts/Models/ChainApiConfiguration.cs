namespace ZinecoMatcher.Contracts.Models
{
    public class ChainApiConfiguration
    {
        public ChainApiOption ZineCoNews { get; set; } = new();
        public ChainApiOption SuperNews { get; set; } = new();
        public ChainApiOption NewsInWord { get; set; } = new();
        public ChainApiOption AdventureNews { get; set; } = new();
    }
}
