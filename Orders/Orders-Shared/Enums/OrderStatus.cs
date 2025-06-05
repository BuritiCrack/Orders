using System.ComponentModel;

namespace Orders_Shared.Enums
{
    public enum OrderStatus
    {
        [Description("Nuevo")]
        New,

        [Description("Despachado")]
        Dispatched,

        [Description("Enviado")]
        Sent,

        [Description("Confirmado")]
        Confirmed,

        [Description("Cancelado")]
        Cancelled
    }
}