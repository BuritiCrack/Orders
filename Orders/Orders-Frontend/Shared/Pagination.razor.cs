using Microsoft.AspNetCore.Components;

namespace Orders_Frontend.Shared
{
    public partial class Pagination
    {
        private List<PageModel> _links = null!;
        [Parameter] public int TotalPages { get; set; }
        [Parameter] public int ItemsPerPage { get; set; } = 10;
        [Parameter] public int CurrentPage { get; set; } = 1;
        [Parameter] public EventCallback<int> SelectedPage { get; set; }

        protected override void OnParametersSet()
        {
            _links = new List<PageModel>();
            // Creamos el boton anterior
            _links.Add(new PageModel
            {
                Text = "Anterior",
                Page = CurrentPage - 1,
                IsEnable = CurrentPage > 1
            });

            for (int i = 1; i <= TotalPages; i++)
            {
                if (TotalPages <= ItemsPerPage)
                {
                    _links.Add(new PageModel
                    {
                        Page = i,
                        IsEnable = CurrentPage == i,
                        Text = $"{i}"
                    });
                }

                if (TotalPages > ItemsPerPage && i <= ItemsPerPage && CurrentPage <= ItemsPerPage)
                {
                    _links.Add(new PageModel
                    {
                        Page = i,
                        IsEnable = CurrentPage == i,
                        Text = $"{i}"
                    });
                }

                if (CurrentPage > ItemsPerPage && i > CurrentPage - ItemsPerPage && i <= CurrentPage)
                {
                    _links.Add(new PageModel
                    {
                        Page = i,
                        IsEnable = CurrentPage == i,
                        Text = $"{i}"
                    });
                }
            }

            // Creamos el boton siguiente
            _links.Add(new PageModel
            {
                Text = "Siguiente",
                Page = CurrentPage != TotalPages ? CurrentPage + 1 : CurrentPage,
                IsEnable = CurrentPage != TotalPages
            });
        }

        private async Task InternalSelectPage(PageModel pageModel)
        {
            if (pageModel.Page == CurrentPage || pageModel.Page == 0)
            {
                return;
            }
            await SelectedPage.InvokeAsync(pageModel.Page);
        }
        private class PageModel
        {
            public string Text { get; set; } = null!;
            public int Page { get; set; } // El número de página
            public bool IsEnable { get; set; } // Indica si el botón está habilitado
        }
    }
}