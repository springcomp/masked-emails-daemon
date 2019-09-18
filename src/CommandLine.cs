using System;
using Microsoft.Extensions.Logging;

namespace maskedd
{
    public sealed class CommandLine
    {
        public LogLevel Level { get; set; }

        private CommandLine()
        {
            Level = LogLevel.Information;
        }

        public static CommandLine Parse(string[] args)
        {
            var cmdLine = new CommandLine();

            // args are : <log-level[int]>
            // TODO: replace with args parsing

            if (args.Length > 0)
            {
                var level = (int)cmdLine.Level;
                if (Int32.TryParse(args[0], out level))
                {
                    cmdLine.Level = (LogLevel)level;
                }
            }

            return cmdLine;
        }
    }
}
