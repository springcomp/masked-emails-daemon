using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MaskedEmails.Commands
{
    public partial class MaskedEmailCommandJsonConvert
    {
        public static MaskedEmailCommand DeserializeObject(string text)
        {
            var settings = new JsonSerializerSettings
            {
                Converters = {
                    new MaskedEmailCommandJsonConverter(),
                    new StringEnumConverter(),
                },
            };
            return JsonConvert.DeserializeObject<MaskedEmailCommand>(text, settings);
        }

        public static string SerializeObject(MaskedEmailCommand command)
        {
            var settings = new JsonSerializerSettings
            {
                Converters =
                {
                    new StringEnumConverter(),
                },
            };
            return JsonConvert.SerializeObject(command, settings);
        }
    }
}