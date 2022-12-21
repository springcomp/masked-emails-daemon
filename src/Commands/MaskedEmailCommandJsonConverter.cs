using System;
using MaskedEmails.Commands.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MaskedEmails.Commands
{
    public partial class MaskedEmailCommandJsonConvert
    {
        internal sealed class MaskedEmailCommandJsonConverter : AbstractJsonConverter<MaskedEmailCommand>
        {
            protected override MaskedEmailCommand Create(Type objectType, JObject jObject)
            {
                var json = jObject.ToString();

                if (jObject.StringExists("command"))
                {
                    var command = jObject["command"].Value<string>();
                    if (!TryParseEnum(command, out var action))
                        throw new NotSupportedException();

                    switch (action)
                    {
                        case MaskedEmailAction.AddMaskedEmail:
                            return JsonConvert.DeserializeObject<AddMaskedEmailCommand>(json);
                        case MaskedEmailAction.EnableMaskedEmail:
                            return JsonConvert.DeserializeObject<EnableMaskedEmailCommand>(json);
                        case MaskedEmailAction.DisableMaskedEmail:
                            return JsonConvert.DeserializeObject<DisableMaskedEmailCommand>(json);
                        case MaskedEmailAction.RemoveMaskedEmail:
                            return JsonConvert.DeserializeObject<RemoveMaskedEmailCommand>(json);
                        case MaskedEmailAction.ChangeMaskedEmailPassword:
                            return JsonConvert.DeserializeObject<ChangeMaskedEmailPasswordCommand>(json);
                        case MaskedEmailAction.SendMail:
                            return JsonConvert.DeserializeObject<SendMailCommand>(json);
                        default:
                            throw new NotSupportedException();
                    }
                }

                throw Error.InvalidMaskedEmailCommandFormat(json);
            }

            private static bool TryParseEnum(string enumMember, out MaskedEmailAction action)
            {
                action = MaskedEmailAction.Unknown;

                try
                {
                    action = JsonConvert.DeserializeObject<MaskedEmailAction>("\"" + enumMember + "\"", new StringEnumConverter());
                    return true;
                }
                catch(JsonSerializationException)
                {
                    return false;
                }
            }
        }
    }
}
