using Microsoft.AspNetCore.Components;

namespace Orders_Frontend.Shared
{
    public partial class Pagination
    {
        private List<PageModel> _links = [];
        private List<OptionModel> _options = [];
        private int selectedOptionValue = 10;
        [Parameter] public int TotalPages { get; set; }
        [Parameter] public int ItemsPerPage { get; set; } = 10;
        [Parameter] public int CurrentPage { get; set; } = 1;
        [Parameter] public EventCallback<int> SelectedPage { get; set; }
        [Parameter] public EventCallback<int> RecordsNumber { get; set; }

        protected override void OnParametersSet()
        {
            BuildPages();
            BuildOptions();
        }

        private void BuildOptions()
        {
            _options =
                [
                new OptionModel {Value = 10, Name = "10"},
                new OptionModel {Value = 25, Name = "25"},
                new OptionModel {Value = 50, Name = "50"},
                new OptionModel {Value = int.MaxValue, Name = "Todos"},
                ];
        }

        private void BuildPages()
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

        private async Task InternalRecordsNumberSelected(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                selectedOptionValue = Convert.ToInt32(e.Value.ToString());
            }

            await RecordsNumber.InvokeAsync(selectedOptionValue);
        }

        private async Task InternalSelectPage(PageModel pageModel)
        {
            if (pageModel.Page == CurrentPage || pageModel.Page == 0)
            {
                return;
            }
            await SelectedPage.InvokeAsync(pageModel.Page);
        }

        private class OptionModel()
        {
            public string Name { get; set; } = null!;
            public int Value { get; set; }
        }
        private class PageModel
        {
            public string Text { get; set; } = null!;
            public int Page { get; set; } // El número de página
            public bool IsEnable { get; set; } // Indica si el botón está habilitado
        }
    }
}