namespace MaskedEmails.Commands
{
    public class EnableMaskedEmailCommand : MaskedEmailCommand
    {
        public EnableMaskedEmailCommand(string address)
            : base(MaskedEmailAction.EnableMaskedEmail, address)
        {
        }
    }
}