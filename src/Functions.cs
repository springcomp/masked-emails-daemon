using System.ComponentModel;
using Microsoft.Azure.Functions.Worker;
using MaskedEmails;
using MaskedEmails.Commands;

namespace maskedd
{
	public sealed class Functions
	{
		private readonly ILogger logger_;

		public Functions(ILoggerFactory loggerFactory)
		{
			logger_ = loggerFactory.CreateLogger<Functions>();
		}

		[Function("on-commands")]
		public void ProcessMaskedEmailCommandAsync(
					[QueueTrigger("commands", Connection = "AzureWebJobsStorage")] string text
				)
		{
			var request = MaskedEmailCommandJsonConvert.DeserializeObject(text);
			if (request == null)
			{
				logger_.LogError("Invalid or malformed request. Ignoring (Enable Trace to include more details).");
				logger_.LogTrace(text);
				return;
			}

			logger_.LogInformation($"Handling {request.Action} request...");

			var commands = MaskedEmailCommandLineFormatter.Format(request);
			foreach (var command in commands)
				Exec(command, logger_);
		}

		[Function("on-commands-poison")]
		public void ProcessMaskedEmailCommandPoisonAsync(
			[QueueTrigger("commands-poison", Connection = "AzureWebJobsStorage")] string text
		)
		{
			logger_.LogWarning("Ignoring requests after too many failed attempts.");
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

			ExecProcess(program, arguments, logger);
		}

		private static void ExecProcess(string program, string arguments, ILogger logger)
		{
			try
			{
				var process = System.Diagnostics.Process.Start(program, arguments);
				if (process == null)
					logger.LogError("The specified command cannot be started.");
				else if (!process.WaitForExit(20000))
					logger.LogError($"The command could not run successfully. Exit code {process.ExitCode}.");
				else
					logger.LogTrace($"Command run successfully. Exit code {process.ExitCode}.");
			}
			catch (Exception e)
			{
				if (e is Win32Exception win32Exception && win32Exception.NativeErrorCode == 2)
				{
					logger.LogError($"The specified process {program} does not exist.");
				}
				else
				{
					throw;
				}
			}
		}
	}
}
