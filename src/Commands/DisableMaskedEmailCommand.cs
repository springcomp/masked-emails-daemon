namespace MaskedEmails.Commands
{
    public class DisableMaskedEmailCommand : MaskedEmailCommand
    {
        public DisableMaskedEmailCommand(string address)
            : base(MaskedEmailAction.DisableMaskedEmail, address)
        {
        }
    }
}