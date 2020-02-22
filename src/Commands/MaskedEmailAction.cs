using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MaskedEmails.Commands
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MaskedEmailAction
    {
        [EnumMember(Value = "add-masked-email")]
        AddMaskedEmail,
        [EnumMember(Value = "enable-masked-email")]
        EnableMaskedEmail,
        [EnumMember(Value = "disable-masked-email")]
        DisableMaskedEmail,
        [EnumMember(Value = "remove-masked-email")]
        RemoveMaskedEmail,
        [EnumMember(Value = "change-masked-email-password")]
        ChangeMaskedEmailPassword,
        [EnumMember(Value = "_this_is_not_a_valid_value_")]
        Unknown,
    }
}