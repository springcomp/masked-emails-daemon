using System;
using System.ComponentModel.Design;
using MaskedEmails.Commands;

namespace MaskedEmails
{
    public static class MaskedEmailCommandLineFormatter
    {
        public static string[] Format(MaskedEmailCommand command)
        {
            if (command is AddMaskedEmailCommand addMaskedEmailCommand)
            {
                return new[]
                {
                    $"/usr/local/bin/add-masked-email -address {command.Address} -passwordHash {addMaskedEmailCommand.PasswordHash} -force",
                    $"/usr/local/bin/set-masked-email -address {command.Address} -forwardTo {addMaskedEmailCommand.AlternateAddress}",
                };
            }
            if (command is RemoveMaskedEmailCommand _)
            {
                return new[] { $"/usr/local/bin/remove-masked-email -address {command.Address} -force", };
            }
            if (command is EnableMaskedEmailCommand _)
            {
                return new[] { $"/usr/local/bin/set-masked-email -address {command.Address} -enable", };
            }
            if (command is DisableMaskedEmailCommand _)
            {
                return new[] { $"/usr/local/bin/set-masked-email -address {command.Address} -disable", };
            }
            if (command is ChangeMaskedEmailPasswordCommand changeMaskedEmailPasswordCommand)
            {
                return new[] { $"/usr/local/bin/change-masked-email-password -address {command.Address} -passwordHash {changeMaskedEmailPasswordCommand.PasswordHash} -force", };
            }
            if (command is SendMailCommand sendMailCommand)
            {
                return new[] { $"/usr/local/bin/send-email -address {command.Address} -subject {sendMailCommand.Subject} -message {sendMailCommand.Message}", };
            }

            System.Diagnostics.Debug.Assert(false);
            throw new NotImplementedException();
        }
    }
}
