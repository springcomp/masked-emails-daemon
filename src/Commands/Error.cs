using System;

namespace MaskedEmails.Commands
{
    public sealed class Error
    {
        public static Exception InvalidMaskedEmailCommandFormat(string json)
        {
            return new MaskedEmailCommandFormatException("", json);
        }

        public static Exception InvalidMaskedEmailCommandAction(string command)
        {
            return new MaskedEmailCommandActionException(command);
        }
    }
}