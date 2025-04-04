using Microsoft.AspNetCore.Components;

namespace Orders_Frontend.Shared
{
    public partial class GenericList<Titem>
    {
        [Parameter] public RenderFragment? Loading { get; set; }
        [Parameter] public RenderFragment? NoRecord { get; set; }
        [Parameter, EditorRequired] public RenderFragment Body { get; set; } = null!;
        [Parameter, EditorRequired] public List<Titem> MyList { get; set; }
    }
}