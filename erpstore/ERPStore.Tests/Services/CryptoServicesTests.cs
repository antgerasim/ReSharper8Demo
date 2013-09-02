using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Microsoft.Practices.Unity;

namespace ERPStore.Tests.Services
{
    [TestFixture]
    public class CryptoServicesTests : TestBase
    {
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
        }

        [Test]
        public void EncryptAccountConfirmation()
        {
            var cryptoService = m_Container.Resolve<ERPStore.Services.CryptoService>();
            var encrypted = cryptoService.EncryptAccountConfirmation("test@email.com", 5);

            Assert.IsNotNull(encrypted);

            string email = null;
            int userId = 0;

            cryptoService.DecryptAccountConfirmation(encrypted, out email, out userId);

            Assert.AreEqual(email, "test@email.com");
            Assert.AreEqual(userId, 5);
        }

        [Test]
        public void ChangePassword()
        {
            var cryptoService = m_Container.Resolve<ERPStore.Services.CryptoService>();
            var encrypted = cryptoService.EncryptChangePassword(5, "test@email.com");

            Assert.IsNotNull(encrypted);

            string email = null;
            int userId = 0;
			DateTime expirationDate = DateTime.MinValue;
            cryptoService.DecryptChangePassword(encrypted, out userId, out email, out expirationDate);

            Assert.AreEqual(email, "test@email.com");
            Assert.AreEqual(userId, 5);
        }

        [Test]
        public void CompleteAccount()
        {
            var cryptoService = m_Container.Resolve<ERPStore.Services.CryptoService>();
            var encrypted = cryptoService.EncryptCompleteAccount(5);

            Assert.IsNotNull(encrypted);

            int userId = 0;

            cryptoService.DecryptCompleteAccount(encrypted, out userId);

            Assert.AreEqual(userId, 5);
        }

        [Test]
        public void OrderDetail()
        {
            var cryptoService = m_Container.Resolve<ERPStore.Services.CryptoService>();
            var encrypted = cryptoService.EncryptOrderConfirmation("12345", DateTime.Now.AddDays(1), false);

            Assert.IsNotNull(encrypted);

            string orderCode = null;
			bool notification = false;
			DateTime expirationDate = DateTime.MinValue;

            cryptoService.DecryptOrderConfirmation(encrypted, out orderCode, out expirationDate, out notification);

            Assert.AreEqual(orderCode, "12345");
        }

    }
}
