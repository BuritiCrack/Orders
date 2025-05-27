using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;

namespace Orders_Frontend.Pages.Auth
{
    public partial class ConfirmEmail
    {
        private string? message;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string UserId { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Token { get; set; } = string.Empty;

        protected async Task ConfirmAccountAsync()
        {
            try
            {
                var responseHttp = await Repository.GetAsync($"/api/accounts/ConfirmEmail/?userId={UserId}&token={Token}");
                if (responseHttp.Error)
                {
                    message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    NavigationManager.NavigateTo("/");
                    return;
                }

                await SweetAlertService.FireAsync("Confirmación", "Gracias por confirmar su email, ahora puedes ingresar al sistema.",
                    SweetAlertIcon.Info);
                NavigationManager.NavigateTo("/Login");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error during email confirmation: {ex.Message}");
                NavigationManager.NavigateTo("/");
            }

        }

    }
}