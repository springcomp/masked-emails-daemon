using Newtonsoft.Json;

namespace MaskedEmails.Commands
{
    public class MaskedEmailCommand
    {
        protected MaskedEmailCommand(MaskedEmailAction action, string address)
        {
            Action = action;
            Address = address;
        }
        [JsonProperty("command")]
        public MaskedEmailAction Action { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}