using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Frontend.Shared;
using Orders_Shared.DTOs;

namespace Orders_Frontend.Pages.Auth
{
    public partial class ChangePassword
    {
        private ChangePasswordDTO changePasswordDTO = new();
        private bool loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;


        private async Task ChangePsswordAsync()
        {
            loading = true;
            var responseHttp = await Repository.PostAsync("api/accounts/changePassword", changePasswordDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error",message, SweetAlertIcon.Error);
                loading = false;
                return;
            }

            loading = true;
            NavigationManager.NavigateTo("/editUser");
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });

            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Contraseña cambiana con éxito.");
        }
    }
}