using Microsoft.AspNetCore.Mvc;
using SM.Domain.Enum;
using SM.Domain.ViewModel;

namespace SM.Application.Extensions
{
    public static class ControllerExtensions
    {
        public static void MostrarMensagem(this Controller @this, string text, bool error = false)
        {
            @this.TempData["message"] = MessageVM.Serializer(
                text, error ? EMessageType.Error : EMessageType.Information);
        }
    }
}