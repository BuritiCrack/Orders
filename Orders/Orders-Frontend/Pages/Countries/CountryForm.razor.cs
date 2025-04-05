using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Orders_Shared.Entities;

namespace Orders_Frontend.Pages.Countries
{
    public partial class CountryForm
    {
        private EditContext _editContext = null!;

        [EditorRequired, Parameter] public Country Country { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; } 
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; }

        // Tenermos que sobreescribir para cargar el contexto
        protected override void OnInitialized()
        {
            _editContext = new(Country);
        }

        // Me sirve para preguntar si me sali del formulario sin guardar los cambios
        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = _editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmacón",
                Text = "Deseas abandonar la páginar y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });

            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }
    }
}