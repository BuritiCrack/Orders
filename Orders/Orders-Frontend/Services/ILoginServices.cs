namespace Orders_Frontend.Services
{
    public interface ILoginServices
    {
        Task LoginAsync(string token);

        Task LogoutAsync();
    }
}