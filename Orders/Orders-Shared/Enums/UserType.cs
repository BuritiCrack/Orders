using System.ComponentModel;

namespace Orders_Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,

        [Description("Usuario")]
        User
    }
}