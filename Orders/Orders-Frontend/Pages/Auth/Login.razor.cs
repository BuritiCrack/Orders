using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Frontend.Services;
using Orders_Shared.DTOs;

namespace Orders_Frontend.Pages.Auth
{
    public partial class Login
    {
        private LoginDTO _loginDTO = new();
        private bool wasClose;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ILoginServices LoginServices { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;


        private async Task LoginAsync()
        {
            if (wasClose)
            {
                NavigationManager.NavigateTo("/");
                return;
            }

            var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", _loginDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            await LoginServices.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }

        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

    }
}