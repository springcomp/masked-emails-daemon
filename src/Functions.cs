using System;
using System.Threading.Tasks;
using MaskedEmails;
using MaskedEmails.Commands;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace maskedd
{
    public sealed class Functions
    {
        public void ProcessMaskedEmailCommandAsync(
            [QueueTrigger("commands")] string text,
            ILogger logger)
        {
            var request = MaskedEmailCommandJsonConvert.DeserializeObject(text);

            logger.LogInformation($"Handling {request.Action} request...");

            var commands = MaskedEmailCommandLineFormatter.Format(request);
            foreach (var command in commands)
                Exec(command, logger);
        }

        private static void Exec(string command, ILogger logger)
        {
            var pos = command.IndexOf(' ');
            if (pos == -1)
            {
                logger.LogError($"Invalid command line: '{command}'");
                return;
            }

            var program = command.Substring(0, pos);
            var arguments = command.Substring(pos + 1);

            logger.LogDebug(command);

            var process = System.Diagnostics.Process.Start(program, arguments);
            if (process == null)
                logger.LogError("The specified command cannot be started.");
            else if (!process.WaitForExit(20000))
                logger.LogError($"The command could not run successfully. Exit code {process.ExitCode}.");
            else
                logger.LogTrace($"Command run successfully. Exit code {process.ExitCode}.");
        }
    }
}