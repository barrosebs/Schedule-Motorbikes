using SM.Domain.Enum;
using System.Text.Json;

namespace SM.Domain.ViewModel
{
    public class MessageVM
    {
        public EMessageType Type { get; set; }
        public string Text { get; set; }
        public MessageVM(string text, EMessageType type = EMessageType.Information)
        {
            Type = type;
            Text = text;
        }

        public static string Serializer(string mensagem, EMessageType tipo = EMessageType.Information)
        {
            var message = new MessageVM(mensagem, tipo);
            return JsonSerializer.Serialize(message);
        }

        public static MessageVM? Deserializer(string messageStr)
        {
            var message = JsonSerializer.Deserialize<MessageVM>(messageStr) ;
            return message;
        }
    }
}
