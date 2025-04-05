using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Repositories;
using Orders_Shared.Entities;

namespace Orders_Frontend.Pages.Countries
{
    public partial class CountryCreate
    {
        private Country _country = new();
        private CountryForm? _countryForm;
        [Inject] private IRepository _repository { get; set; } = null!;
        [Inject] private SweetAlertService _sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager  _navigationManager {  get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await _repository.PostAsync("/api/countries", _country);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await _sweetAlertService.FireAsync("Error", message);
                return;
            }

            Return();
            var toast = _sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position  = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3500
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con exito.");

        }

        private void Return()
        {
            _countryForm!.FormPostedSuccessfully = true;
            _navigationManager.NavigateTo("/countries");
        }
    }
}