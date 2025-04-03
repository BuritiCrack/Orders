using System.Net;

namespace Orders_Frontend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var statuscode = HttpResponseMessage.StatusCode;
            if (statuscode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            if (statuscode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            if (statuscode == HttpStatusCode.Unauthorized)
            {
                return "Debes de estar logeado para ejecutar esta accion";
            }
            if (statuscode == HttpStatusCode.Forbidden)
            {
                return "No tienes permiso para ejecutar esta operacion";
            }

            return "Ha ocrurrido un erro inesperado";
        }
    }
}