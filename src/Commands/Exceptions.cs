using System;
using System.Text;

namespace MaskedEmails.Commands
{
    public class MaskedEmailException : Exception
    {
        public MaskedEmailException() { }
        public MaskedEmailException(string message) : base(message) { }
        public MaskedEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class MaskedEmailCommandException : MaskedEmailException
    {
        public MaskedEmailCommandException() { }
        public MaskedEmailCommandException(string message) : base(message) { }
        public MaskedEmailCommandException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class MaskedEmailCommandFormatException : MaskedEmailCommandException
    {
        public MaskedEmailCommandFormatException() { }
        public MaskedEmailCommandFormatException(string message) : base(message) { }
        public MaskedEmailCommandFormatException(string message, Exception innerException) : base(message, innerException) { }

        public MaskedEmailCommandFormatException(string message, string json)
            : base(MakeCompoundMessage(message, json))
        {
            Json = json;
        }

        public string Json { get; set; }

        private static string MakeCompoundMessage(string message, string json)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(message))
                builder.AppendLine(message);
            builder.AppendLine(json);
            return builder.ToString();
        }
    }
    public class MaskedEmailCommandActionException : MaskedEmailCommandException
    {
        public MaskedEmailCommandActionException() { }
        public MaskedEmailCommandActionException(string message) : base(message) { }
        public MaskedEmailCommandActionException(string message, Exception innerException) : base(message, innerException) { }

        public MaskedEmailCommandActionException(MaskedEmailAction action)
            : base($"The value \"{action}\" is not a valid masked email command.")
        {
            Action = action;
        }

        public MaskedEmailAction Action { get; set; }
    }
}