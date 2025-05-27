namespace Orders_Frontend.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<object>> GetAsync(string url);

        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);

        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);

        Task<HttpResponseWrapper<IActionResponse>> PostAsync<T, IActionResponse>(string url, T model);

        Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url);

        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);

        Task<HttpResponseWrapper<IActionResponse>> PutAsync<T, IActionResponse>(string url, T model);
    }
}