using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Frontend.Services;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;
using Orders_Shared.Enums;

namespace Orders_Frontend.Pages.Auth
{
    public partial class Register
    {
        private UserDTO _userDTO = new();
        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;
        private bool loading;
        private string? imageUrl;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ILoginServices LoginServices { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCountriesAsync();
        }

        private void ImageSelected(string imageBase64)
        {
            _userDTO.Photo = imageBase64;
            imageUrl = null;
        }

        private async Task LoadCountriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("/api/countries/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            countries = responseHttp.Response;
        }

        private async Task CountryChangeAsync(ChangeEventArgs e)
        {
            var selectedCountry = Convert.ToInt32(e.Value!);
            states = null;
            cities = null;
            _userDTO.CityId = 0;
            await LoadStatesAsync(selectedCountry);
        }

        private async Task LoadStatesAsync(int countryId)
        {
            var responseHttp = await Repository.GetAsync<List<State>>($"/api/states/combo/{countryId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            states = responseHttp.Response;
        }

        private async Task StateChangeAsync(ChangeEventArgs e)
        {
            var selectedState = Convert.ToInt32(e.Value!);
            cities = null;
            _userDTO.CityId = 0;
            await LoadCitiesAsync(selectedState);
        }

        private async Task LoadCitiesAsync(int stateId)
        {
            var responseHttp = await Repository.GetAsync<List<City>>($"/api/cities/combo/{stateId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            cities = responseHttp.Response;
        }

        private async Task CreateUserAsync()
        {
            _userDTO.UserName = _userDTO.Email;
            _userDTO.UserType = UserType.User;

            loading = true;
            var responseHttp = await Repository.PostAsync<UserDTO>("/api/accounts/CreateUser", _userDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Su cuenta ha sido creada con éxito." +
                "Se te ha enviado un correo de confirmacion con la instrucciones para activar su usuario.",
                SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");
        }
    }
}