using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Frontend.Services;
using Orders_Shared.DTOs;

namespace Orders_Frontend.Pages.Auth
{
    public partial class ResendConfirmationEmailToken
    {
        private EmailDTO _emailDTO = new();
        private bool _loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private async Task ResendConfirmationEmailTokenAsync()
        {
            _loading = true;
            var responseHttp = await Repository.PostAsync("api/accounts/ResendToken", _emailDTO);
            _loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error",message,SweetAlertIcon.Error);
                _loading =false;
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Se te ha enviado un correo con las" +
                "instrucciones para activar tu usuario", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");
        }
    }
}