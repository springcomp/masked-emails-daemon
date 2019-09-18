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
                    var action = ParseEnum(command);
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
                        default:
                            System.Diagnostics.Debug.Assert(false);
                            throw Error.InvalidMaskedEmailCommandAction(command);
                    }
                }

                throw Error.InvalidMaskedEmailCommandFormat(json);
            }

            private static MaskedEmailAction ParseEnum(string enumMember)
            {
                return JsonConvert.DeserializeObject<MaskedEmailAction>("\"" + enumMember + "\"", new StringEnumConverter());
            }
        }
    }
}