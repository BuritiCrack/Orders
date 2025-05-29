using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Shared.DTOs;

namespace Orders_Frontend.Pages.Auth
{
    public partial class ResetPassword
    {
        private RecoverDTO _recoverDTO = new();
        private bool _loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter, SupplyParameterFromQuery] public string Token { get; set; } = string.Empty;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        private async Task ChangePasswordAsync()
        {
            _recoverDTO.Token = Token;
            _loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/ResetPassword", _recoverDTO);
            _loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                _loading = false;
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Contraseña cambiada con éxito, " +
                "ahora puede ingresar con su nueva contraseña.", SweetAlertIcon.Info);
            Modal.Show<Login>();

        }

    }
}