namespace ZinecoMatcher.Infrastructure.ApiClient
{
    public interface IApiClient
    {
        Task<List<T>> GetAsync<T>(Uri path);
    }
}
