using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders_Shared.Responses
{
    public class ActionResponse<T>
    {
        // Si la respuesta es exitosa devuelve retultado sino devuelve mensaje
        public bool WasSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
