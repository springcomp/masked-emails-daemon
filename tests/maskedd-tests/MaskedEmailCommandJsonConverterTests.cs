using MaskedEmails.Commands;
using NUnit.Framework;
using System;
using System.Text;

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
            Assert.AreEqual(command.Action, o.Action);
            Assert.AreEqual(command.Address, o.Address);

            var c = o as AddMaskedEmailCommand;
            Assert.AreEqual(command.PasswordHash, c.PasswordHash);
            Assert.AreEqual(command.AlternateAddress, c.AlternateAddress);
        }
        [Test]
        public void RemoveMaskedEmailCommand_DeserializeObject()
        {
            var command = new RemoveMaskedEmailCommand("m123456@domain.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(command.Action, o.Action);
            Assert.AreEqual(command.Address, o.Address);

            var c = o as RemoveMaskedEmailCommand;
            Assert.NotNull(c);
        }
        [Test]
        public void EnableMaskedEmail_DeserializeObject()
        {
            var command = new EnableMaskedEmailCommand("m123456@domain.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(command.Action, o.Action);
            Assert.AreEqual(command.Address, o.Address);

            var c = o as EnableMaskedEmailCommand;
            Assert.NotNull(c);
        }
        [Test]
        public void DisableMaskedEmail_DeserializeObject()
        {
            var command = new DisableMaskedEmailCommand("m123456@domain.com");

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(command.Action, o.Action);
            Assert.AreEqual(command.Address, o.Address);

            var c = o as DisableMaskedEmailCommand;
            Assert.NotNull(c);
        }
        [Test]
        public void SendEmail_DeserializeObject()
        {
            var base64 = 
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes("<h1>Title</h1><p>This is the mail body.</p>")
                );

            var command = new SendMailCommand(
                    "alice@example.com",
                    subject: "subject",
                    message: base64
                    );

            var text = MaskedEmailCommandJsonConvert.SerializeObject(command);

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(command.Action, o.Action);
            Assert.AreEqual(command.Address, o.Address);

            var c = o as SendMailCommand;
            Assert.AreEqual(command.Subject, c.Subject);
            Assert.AreEqual(command.Message, c.Message);
        }
        [Test]
        public void SendEmail_DeserializeText()
        {
            const string text = @"{
  ""command"": ""send-email"",
  ""sender"": ""postmaster@masked-emails.me"",
  ""address"": ""bob@example.com"",
  ""subject"": ""a sample email"",
  ""message"": ""MA=""
}";

            var o = MaskedEmailCommandJsonConvert.DeserializeObject(text);
            Assert.AreEqual(MaskedEmailAction.SendMail, o.Action);
            Assert.AreEqual("bob@example.com", o.Address);

            var c = o as SendMailCommand;
            Assert.AreEqual("a sample email", c.Subject);
            Assert.AreEqual("MA=", c.Message);
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