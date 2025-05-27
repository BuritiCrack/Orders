using Orders_Shared.Responses;

namespace Orders_Backend.Helpers
{
    public interface IMailHelper
    {
        ActionResponse<string> SendMail(string toName, string toEmail, string subject,string body);
    }
}
