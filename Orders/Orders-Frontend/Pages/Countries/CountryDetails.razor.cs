﻿using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Orders_Frontend.Pages.States;
using Orders_Frontend.Repositories;
using Orders_Shared.Entities;
using System.Net;
using System.Runtime.CompilerServices;

namespace Orders_Frontend.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountryDetails
    {
        private Country? country;
        private List<State>? states;
        private int currentPage = 1;
        private int totalPages;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter, SupplyParameterFromQuery] public string? Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string? Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 10;
        [CascadingParameter] IModalService Modal { get; set; } = default!;



        [Parameter] public int CountryId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<StateEdit>(string.Empty, new ModalParameters().Add("StateId", id));
            }
            else
            {
                modalReference = Modal.Show<StateCreate>(string.Empty, new ModalParameters().Add("CountryId", CountryId));
            }

            var result = await modalReference.Result;
            if (result.Confirmed)
            {
                await LoadAsync();
            }
        }


        private async Task SelectedRecordsNumberAsync(int recordsNumber)
        {
            RecordsNumber = recordsNumber;
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }
        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }
        private async Task SelectedPageAsync(int page)
        {

            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }
            currentPage = page;
            await LoadAsync(page);
        }
        private async Task LoadAsync(int page = 1)
        {
            
            var ok = await LoadCountryAsync();
            if (ok)
            {
                ok = await LoadStatesAsync(page);
                if (ok)
                {
                    await LoadTotalPagesAsync();
                }
            }
        }

        private async Task<bool> LoadCountryAsync()
        {
            var responseHttp = await Repository.GetAsync<Country>($"/api/countries/{CountryId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                    return false;
                }

                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            country = responseHttp.Response;
            return true;

        }

        private async Task<bool> LoadStatesAsync(int page)
        {
            ValidateRecordsNumber(RecordsNumber);
            var url = $"/api/states?id={CountryId}&page={page}&recordsNumber={RecordsNumber}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<List<State>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            states = responseHttp.Response;
            return true;
        }

        private async Task LoadTotalPagesAsync()
        {
            ValidateRecordsNumber(RecordsNumber);
            var url = $"/api/states/totalPages?id={CountryId}&recordsNumber={RecordsNumber}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;

            }
            totalPages = responseHttp.Response;
        }

        private void ValidateRecordsNumber(int recordsNumber)
        {
            if (recordsNumber == 0)
                RecordsNumber = 10;
        }
        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task DeleteAsync(State state)
        {
            var resul = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmacion",
                Text = $"¿Deseas eliminar el departamento/estado? {state.Name}",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                CancelButtonText = "No",
                ConfirmButtonText = "Si"
            });

            var confirm = string.IsNullOrEmpty(resul.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<State>($"/api/states/{state.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });

            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con exito");
            
        }
    }
}