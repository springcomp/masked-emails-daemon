using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Syslog.Framework.Logging;

namespace MaskedEmails.Extensions.Logging.Syslog
{
    public static class SyslogLoggerExtensions
    {
        public static ILoggingBuilder AddSyslog(this ILoggingBuilder builder, HostBuilderContext hostContext)
        {
            var configuration = hostContext.Configuration;
            var section = configuration.GetSection("SyslogSettings");
            if (section != null)
            {
                var settings = new SyslogLoggerSettings();
                section.Bind(settings);
            }

            if (!TryGetSyslogLoggingSectionDefaultLogLevel(configuration, out var level))
                if (!TryGetLoggingSectionDefaultLogLevel(configuration, out level))
                { }

            builder.AddSyslog(section, null, null, level);
            return builder;
        }

        private static bool TryGetLoggingSectionDefaultLogLevel(IConfiguration configuration, out LogLevel level)
        {
            level = LogLevel.Information;

            var logging = configuration.GetSection("Logging");
            return logging != null && GetDefaultLogLevel(logging, out level);
        }

        private static bool TryGetSyslogLoggingSectionDefaultLogLevel(IConfiguration configuration, out LogLevel level)
        {
            level = LogLevel.Information;

            var logging = configuration.GetSection("Logging");
            var syslog = logging?.GetSection("Syslog");
            return syslog != null && GetDefaultLogLevel(syslog, out level);
        }
        private static bool GetDefaultLogLevel(IConfigurationSection logging, out LogLevel level)
        {
            level = LogLevel.Information;

            var logLevelSection = logging.GetSection("LogLevel");
            if (logLevelSection == null)
                return false;

            var defaultLogLevel = (string) logLevelSection.GetValue(typeof(string), "Default") ?? "Information";
            if (!Enum.TryParse(typeof(LogLevel), defaultLogLevel, out var parsed))
                return false;

            level = (LogLevel) parsed;
            return true;
        }

    }
}