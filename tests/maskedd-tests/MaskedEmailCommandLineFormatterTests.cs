using MaskedEmails;
using MaskedEmails.Commands;
using NUnit.Framework;

namespace maskedd_tests
{
    [TestFixture]
    public class MaskedEmailCommandLineFormatterTests
    {
        [Test]
        public void AddMaskedEmailCommand_FormatCommandLine()
        {
            var command = new AddMaskedEmailCommand("m123456@domain.com",
                "{SSHA512}XXtbZp4Gg8cTjH9p/1LtebnYLRaVJ15QJ7oFujjqQtrzUh/bVvC4zUHa5dyrqS0tbOLgIk5RlKj2gZ/4uwymY1JVTXQ=",
                "forward.to@example.com"
            );
            var commandLines = MaskedEmailCommandLineFormatter.Format(command);

            Assert.AreEqual(2, commandLines.Length);
            Assert.AreEqual("/usr/local/bin/add-masked-email -address m123456@domain.com -passwordHash {SSHA512}XXtbZp4Gg8cTjH9p/1LtebnYLRaVJ15QJ7oFujjqQtrzUh/bVvC4zUHa5dyrqS0tbOLgIk5RlKj2gZ/4uwymY1JVTXQ= -force", commandLines[0]);
            Assert.AreEqual("/usr/local/bin/set-masked-email -address m123456@domain.com -forwardTo forward.to@example.com", commandLines[1]);
        }
        [Test]
        public void RemoveMaskedEmailCommand_FormatCommandLine()
        {
            var command = new RemoveMaskedEmailCommand("m123456@domain.com");
            var commandLines = MaskedEmailCommandLineFormatter.Format(command);

            Assert.AreEqual(1, commandLines.Length);
            Assert.AreEqual("/usr/local/bin/remove-masked-email -address m123456@domain.com -force", commandLines[0]);
        }
        [Test]
        public void EnableMaskedEmailCommand_FormatCommandLine()
        {
            var command = new EnableMaskedEmailCommand("m123456@domain.com");
            var commandLines = MaskedEmailCommandLineFormatter.Format(command);

            Assert.AreEqual(1, commandLines.Length);
            Assert.AreEqual("/usr/local/bin/set-masked-email -address m123456@domain.com -enable", commandLines[0]);
        }
        [Test]
        public void DisableMaskedEmailCommand_FormatCommandLine()
        {
            var command = new DisableMaskedEmailCommand("m123456@domain.com");
            var commandLines = MaskedEmailCommandLineFormatter.Format(command);

            Assert.AreEqual(1, commandLines.Length);
            Assert.AreEqual("/usr/local/bin/set-masked-email -address m123456@domain.com -disable", commandLines[0]);
        }
    }
}