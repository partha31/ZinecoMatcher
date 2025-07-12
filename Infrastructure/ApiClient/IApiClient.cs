namespace ZinecoMatcher.Infrastructure.ApiClient
{
    public interface IApiClient
    {
        Task<List<T>> GetAsync<T>(string path);
    }
}
