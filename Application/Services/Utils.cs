using System.Text.RegularExpressions;
using ZinecoMatcher.Contracts.Models;

namespace ZinecoMatcher.Application.Services
{
    public static class Utils
    {

        // Remove punctuation from the string. Got this regex expression from ChatGPT.
        public static string NormalizeString(string input) =>
            Regex.Replace(input.ToLowerInvariant(), @"[^\w\s]", "").Replace("  ", " ").Trim();

        public static string GetFullAddress(NewsAgent agent) => agent.Address1 + agent.Address2 ?? ""
            + agent.City + agent.State + agent.PostCode;
    }
}
