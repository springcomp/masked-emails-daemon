using MaskedEmails.Commands;
using NUnit.Framework;

namespace maskedd_tests
{
    [TestFixture]
    public class MaskedEmailCommandJsonConverterTests
    {
        [Test]
        public void AddMaskedEmailCommand_DeserializeObject()
        {
            var command = new AddMaskedEmailCommand("m123456@domain.com", "hash", "forward.to@example.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(o.Action, command.Action);

            var c = o as AddMaskedEmailCommand;
            Assert.AreEqual(command.Address, c.Address);
            Assert.AreEqual(command.PasswordHash, c.PasswordHash);
            Assert.AreEqual(command.AlternateAddress, c.AlternateAddress);
        }
        [Test]
        public void RemoveMaskedEmailCommand_DeserializeObject()
        {
            var command = new RemoveMaskedEmailCommand("m123456@domain.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(o.Action, command.Action);

            var c = o as RemoveMaskedEmailCommand;
            Assert.AreEqual(command.Address, c.Address);
        }
        [Test]
        public void EnableMaskedEmail_DeserializeObject()
        {
            var command = new EnableMaskedEmailCommand("m123456@domain.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(o.Action, command.Action);

            var c = o as EnableMaskedEmailCommand;
            Assert.AreEqual(command.Address, c.Address);
        }
        [Test]
        public void DisableMaskedEmail_DeserializeObject()
        {
            var command = new DisableMaskedEmailCommand("m123456@domain.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(o.Action, command.Action);

            var c = o as DisableMaskedEmailCommand;
            Assert.AreEqual(command.Address, c.Address);
        }
        [Test]
        public void ChangeMaskedEmailPassword_DeserializeObject()
        {
            var command = new ChangeMaskedEmailPasswordCommand("m123456@domain.com"){
                PasswordHash = "password-hash",
            };

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(o.Action, command.Action);

            var c = o as ChangeMaskedEmailPasswordCommand;
            Assert.AreEqual(command.Address, c.Address);
            Assert.AreEqual(command.PasswordHash, c.PasswordHash);
        }
    }
}