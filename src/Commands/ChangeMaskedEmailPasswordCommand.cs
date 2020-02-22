using Newtonsoft.Json;

namespace MaskedEmails.Commands
{
    public class ChangeMaskedEmailPasswordCommand : MaskedEmailCommand
    {
        public ChangeMaskedEmailPasswordCommand(string address)
            : base(MaskedEmailAction.ChangeMaskedEmailPassword, address)
        {
        }

        [JsonProperty("password-hash")]
        public string PasswordHash { get; set; }
    }
}