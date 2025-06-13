using Orders_Shared.Responses;

namespace Orders_Backend.Helpers
{
    public interface IOrdersHelper
    {
        Task<ActionResponse<bool>> ProcessOrderAsync(string email, string remarks);
    }
}
