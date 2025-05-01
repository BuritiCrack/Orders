using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Frontend.Services;
using Orders_Shared.DTOs;
using Orders_Shared.Enums;

namespace Orders_Frontend.Pages.Auth
{
    public partial class Register
    {
        private UserDTO _userDTO = new();

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ILoginServices LoginServices { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private async Task CreateUserAsync()
        {
            _userDTO.UserName = _userDTO.Email;
            _userDTO.UserType = UserType.User;
            var responseHttp = await Repository.PostAsync<UserDTO, TokenDTO>("/api/accounts/CreateUser", _userDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginServices.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }
    }
}