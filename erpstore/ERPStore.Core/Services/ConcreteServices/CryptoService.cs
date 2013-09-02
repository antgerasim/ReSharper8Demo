using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
    /// <summary>
    /// Encryption des differentes parties d'url communiquées au client 
    /// </summary>
	public class CryptoService
	{
		SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider> m_Crypto;

		public CryptoService(byte[] cryptoKey, byte[] cryptoIV)
		{
			m_Crypto = new SymmetricCryptography<System.Security.Cryptography.RC2CryptoServiceProvider>(cryptoKey, cryptoIV);
		}

		public string EncryptAccountConfirmation(string email, int userId)
		{
			var ticket = new { Email = email, UserId = userId, };
			var result = Encrypt(ticket);

			return result;
		}

		public void DecryptAccountConfirmation(string input, out string email, out int userId)
		{
			var ticket = new { Email = string.Empty, UserId = 0};
			var result = Decrypt(input, ticket);
			email = Convert.ToString(result[0]);
			userId = Convert.ToInt32(result[1]);
		}

		internal string GetPublicOrderTicket(Models.Order order)
		{
			var ticket = new { OrderCode = order.Code, OrderType = (int)order.Document, };
			return Encrypt(ticket);
		}

        public string EncryptOrderConfirmation(string orderCode, DateTime expirationDate, bool needNotification)
        {
            var ticket = new { OrderCode = orderCode, Expiration = expirationDate, Notification = needNotification };
            return Encrypt(ticket);
        }

		public void DecryptOrderConfirmation(string input, out string orderCode, out DateTime expirationDate, out bool needNotification)
		{
			var ticket = new { OrderCode = string.Empty, ExpirationDate = DateTime.MaxValue, Notification = false };
			var result = Decrypt(input, ticket);
			orderCode = Convert.ToString(result[0]);
			expirationDate = Convert.ToDateTime(result[1]);
			needNotification = Convert.ToBoolean(result[2]);
		}

		public string EncryptChangePassword(int userId, string userEmail)
		{
			var ticket = new { UserId = userId, UserEmail = userEmail, ExpirationDate = DateTime.Now.AddDays(1) };
			return Encrypt(ticket);
		}

		public void DecryptChangePassword(string input, out int userId, out string userEmail, out DateTime expirationDate)
		{
			var ticket = new { UserId = 0, UserEmail = string.Empty, ExpirationDate = DateTime.Now };
			var result = Decrypt(input, ticket);
			userId = Convert.ToInt32(result[0]);
			userEmail = Convert.ToString(result[1]);
			expirationDate = Convert.ToDateTime(result[2]);
		}

        public string EncryptQuoteConfirmation(string code)
        {
            var ticket = new { code = code , salt = DateTime.Now };
            var result = Encrypt(ticket);
            return result;
        }

        public void DecryptQuoteConfirmation(string input, out string code)
        {
            var ticket = new { code = string.Empty };
            var result = Decrypt(input, ticket);
            code = Convert.ToString(result[0]);
        }

        public string EncryptCompleteAccount(int userId)
        {
            var ticket = new { userId = userId };
            var result = Encrypt(ticket);
            return result;
        }

        public void DecryptCompleteAccount(string key, out int userId)
        {
            var ticket = new { userId = 0 };
            var result = Decrypt(key, ticket);
            userId = Convert.ToInt32(result[0]);
        }

		public string EncryptDocumentDownload(string type, int id)
		{
			var ticket = new { type = type, Id = id, Salt = DateTime.Now };
			var result = Encrypt(ticket);
			return result;
		}

		public void DecryptDocumentDownload(string key, out string type, out int id)
		{
			var ticket = new { type = string.Empty, Id = 0 };
			var result = Decrypt(key, ticket);
			type = result[0].ToString();
			id = Convert.ToInt32(result[1]);
		}

		internal string EncryptInvoiceDetail(string key, string code, DateTime expirationDate, bool notification)
		{
			var ticket = new { Code = code, ExpirationDate = expirationDate, Notification = notification };
			var result = Encrypt(ticket);
			return result;
		}

		internal void DecryptInvoiceDetail(string key, out string code, out DateTime expirationDate, out bool notification)
		{
			var ticket = new { Code = string.Empty, ExpirationDate = DateTime.MinValue, Notification = false };
			var result = Decrypt(key, ticket);
			code = result[0].ToString();
			expirationDate = Convert.ToDateTime(result[1]);
			notification = Convert.ToBoolean(result[2]);
		}

		internal string EncryptAdminKey(string key, DateTime expirationDate)
		{
			var ticket = new { Key = key, ExpirationDate = expirationDate };
			var result = Encrypt(ticket);
			return result;
		}

		internal void DecryptAdminKey(string key, out string adminKey, out DateTime expirationDate)
		{
			var ticket = new { Key = string.Empty, ExpirationDate = DateTime.MinValue };
			var result = Decrypt(key, ticket);
			adminKey = result[0].ToString();
			expirationDate = Convert.ToDateTime(result[1]);
		}

		#region Helper

		public string Encrypt(object subject)
		{
			var properties = from property
							 in subject.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
							 select new
							 {
								 Name = property.Name,
								 Value = property.GetValue(subject, null),
								 Type = property.PropertyType
							 };
			string separtator = "";
			string result = null;
			foreach (var item in properties)
			{
				string pattern = "{0}{1}";
				if (item.Type == typeof(DateTime))
				{
					pattern = "{0}{1:yyMMdd}";
				}
				result += string.Format(pattern, separtator, item.Value);
				separtator = "|";
			}
			result = m_Crypto.Encrypt(result);
			return result; // System.Web.HttpUtility.UrlEncode(result);
		}

		public List<object> Decrypt(string encrypted, object subject)
		{
			if (encrypted.IsNullOrTrimmedEmpty())
			{
				return null;
			}
            // encrypted = System.Web.HttpUtility.UrlDecode(encrypted);
			string input = m_Crypto.Decrypt(encrypted);

			string[] token = input.Split('|');

			var properties = subject.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			var result = new List<object>();
			for (int i = 0; i < properties.Length; i++)
			{
				var pi = properties[i];
				object o = null;
				if (pi.PropertyType == typeof(DateTime))
				{
					string d = token[i];
					int year = 2000 + Convert.ToInt32(d.Substring(0, 2));
					int month = Convert.ToInt32(d.Substring(2, 2));
					int day = Convert.ToInt32(d.Substring(4, 2));
					o = new DateTime(year, month, day);
				}
				else if (pi.PropertyType == typeof(bool)
					&& token[i] == "1")
				{
					o = token[i] == "1" ? "True" : "False";
				}
				else
				{
					o = System.Convert.ChangeType(token[i], pi.PropertyType);
				}
				result.Add(o);
				if (properties[i].CanWrite)
				{
					properties[i].SetValue(subject, o, null);
				}
			}
			return result;
		}

		#endregion

	}
}
