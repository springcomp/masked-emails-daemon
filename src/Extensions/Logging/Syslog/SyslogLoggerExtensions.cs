using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Syslog.Framework.Logging;

namespace MaskedEmails.Extensions.Logging.Syslog
{
    public static class SyslogLoggerExtensions
    {
        public static ILoggingBuilder AddSyslog(this ILoggingBuilder builder, LogLevel level)
        {
            var settings = new SyslogLoggerSettings();
            var syslogLoggerProvider = new SyslogLoggerProvider(
                settings,
                Environment.MachineName,
                LogLevel.Debug
            );

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider>(syslogLoggerProvider));

            return builder;
        }
    }
}