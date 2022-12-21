using Newtonsoft.Json;

namespace MaskedEmails.Commands
{
    public sealed class AddMaskedEmailCommand : MaskedEmailCommand
    {
        public AddMaskedEmailCommand(string address, string passwordHash, string forwardTo)
            : base(MaskedEmailAction.AddMaskedEmail, address)
        {
            PasswordHash = passwordHash;
            AlternateAddress = forwardTo;
        }

        [JsonProperty("password-hash")]
        public string PasswordHash { get; set; }
        [JsonProperty("forward-to")]
        public string AlternateAddress { get; set; }
    }
}