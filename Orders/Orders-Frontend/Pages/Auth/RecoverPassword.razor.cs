using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Frontend.Shared;
using Orders_Shared.DTOs;

namespace Orders_Frontend.Pages.Auth
{
    public partial class RecoverPassword
    {
        private EmailDTO _emailDTO = new();
        private bool _loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private async Task SendRecoverPasswordEmailTokenAsync()
        {
            _loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/RecoverPassword", _emailDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                _loading = false;
                return;
            }

            
            _loading = false;
            await SweetAlertService.FireAsync("Confirmaci�n", "Se te ha enviado un correo electr�nico" +
                " con las instrucciones para recuperar su contrase�a.", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");

        }

    }
}