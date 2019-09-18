namespace MaskedEmails.Commands
{
    public class RemoveMaskedEmailCommand : MaskedEmailCommand
    {
        public RemoveMaskedEmailCommand(string address)
            : base(MaskedEmailAction.RemoveMaskedEmail, address)
        {
        }
    }
}