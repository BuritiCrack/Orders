using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Orders_Frontend.Shared
{
    public partial class InputImg
    {
        private string? imageBase64;

        [Parameter] public string Label { get; set; } = "Imagen";

        [Parameter] public string? ImageURL { get; set; }

        [Parameter] public EventCallback<string> ImageSelected { get; set; }

        private async Task Onchange(InputFileChangeEventArgs e)
        {
            var imagenes = e.GetMultipleFiles();

            foreach (var imagen in imagenes)
            {
                var arraBytes = new byte[imagen.Size];
                await imagen.OpenReadStream().ReadAsync(arraBytes);
                imageBase64 = Convert.ToBase64String(arraBytes);
                ImageURL = null;
                await ImageSelected.InvokeAsync(imageBase64);
                StateHasChanged();
            }
        }
    }
}