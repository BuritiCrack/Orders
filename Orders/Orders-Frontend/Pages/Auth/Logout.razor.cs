using Microsoft.AspNetCore.Components;
using Orders_Frontend.Services;

namespace Orders_Frontend.Pages.Auth
{
    public partial class Logout
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ILoginServices LoginServices { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoginServices.LogoutAsync();
            NavigationManager.NavigateTo("/");
        }
    }
}