namespace MaskedEmails.Commands
{
    public class SendMailCommand : MaskedEmailCommand
    {
        public SendMailCommand(string recipient, string subject, string message)
            : base(MaskedEmailAction.SendMail, recipient)
        {
            Subject = subject;
            Message = message;
        }

        public string Subject { get; }
        public string Message { get; }
    }
}