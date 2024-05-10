using SM.Domain.Enum;
using System.Text.Json;

namespace SM.Domain.ViewModel
{
    public class MessageVM
    {
        public EMessageType Type { get; set; }
        public string Text { get; set; }
        public MessageVM(string message, EMessageType type = EMessageType.Information)
        {
            Type = type;
            Text = message;
        }

        public static string Serializer(string mensagem, EMessageType tipo = EMessageType.Information)
        {
            var mensagemModel = new MessageVM(mensagem, tipo);
            return JsonSerializer.Serialize(mensagemModel);
        }

        public static MessageVM Deserializer(string mensagemString)
        {
            var returnMessage = JsonSerializer.Deserialize<MessageVM>(mensagemString) ?? new MessageVM(mensagemString);
            return returnMessage;
        }
    }
}
