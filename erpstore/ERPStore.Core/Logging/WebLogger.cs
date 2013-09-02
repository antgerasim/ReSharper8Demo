using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;

namespace ERPStore.Logging
{
	internal class WebLogger : ILogger
	{
		public WebLogger()
		{

		}

		public void Info(string message)
		{
			// message = GetPrefix("Info") + message;
            System.Diagnostics.Trace.TraceInformation(message);
		}

		public void Info(string message, params object[] prms)
		{
			// message = GetPrefix("Info") + message;
            System.Diagnostics.Trace.TraceInformation(message, prms);
		}

		public void Notification(string message)
		{
			// message = GetPrefix("Info") + message;
			System.Diagnostics.Trace.TraceWarning(message);
		}

		public void Notification(string message, params object[] prms)
		{
			// message = GetPrefix("Info") + message;
			System.Diagnostics.Trace.TraceWarning(message, prms);
		}

		public void Warn(string message)
		{
			// message = GetPrefix("Warn") + message;
            System.Diagnostics.Trace.TraceWarning(message);
		}

		public void Warn(string message, params object[] prms)
		{
			// message = GetPrefix("Warn") + message;
            System.Diagnostics.Trace.TraceWarning(message,prms);
		}

		public void Debug(string message)
		{
			// System.Diagnostics.Trace.TraceWarning("Debug", message);
			SendMessage("Debug", message);
		}

		public void Debug(string message, params object[] prms)
		{
			// System.Diagnostics.Trace.TraceWarning("Debug", message, prms);
			SendMessage("Debug", message, prms);
		}

		public void Error(string message)
		{
			SendMessage("Error",message);
		}

		public void Error(string message, params object[] prms)
		{
			SendMessage("Error", message, prms);
		}

		public void Error(Exception x)
		{
			string message = GetContent(x);
			SendMessage("Error", message);
		}

		public void Fatal(string message)
		{
			SendMessage("Fatal", message);
		}

		public void Fatal(string message, params object[] prms)
		{
			SendMessage("Fatal", message, prms);
		}

		public void Fatal(Exception x)
		{
			string message = x.ToString();
			foreach (KeyValuePair<string, object> item in x.Data)
			{
				message += string.Format("{0} :{1}\r\n", item.Key, item.Value);
			}
			SendMessage("Fatal", message);
		}

		static string GetContent(Exception ex)
		{
			var content = new StringBuilder();
			content.Append("--------------------------------------------");
			content.AppendLine();
			content.AppendLine(ex.Message);
			content.AppendLine("--------------------------------------------");

			// Ajout des extensions d'erreur
			if (ex.Data != null && ex.Data.Count > 0)
			{
				foreach (var item in ex.Data.Keys)
				{
					content.AppendFormat("{0} = {1}", item, ex.Data[item]);
					content.AppendLine();
				}
			}
			content.Append(ex.StackTrace);
			content.AppendLine();
			if (ex.InnerException != null)
			{
				content.Append("--------------------------------------------");
				content.AppendLine();
				content.Append("Inner Exception");
				content.AppendLine();
				content.Append(GetContent(ex.InnerException));
			}
			return content.ToString();
		}


        private System.Diagnostics.TextWriterTraceListener m_Out;

		public System.IO.TextWriter Out
		{
			get
			{
				if (m_Out == null)
				{
                    m_Out = new System.Diagnostics.TextWriterTraceListener();// new ConsoleTextWriter();
				}
				return m_Out.Writer;
			}
		}

		public void Watch(string title, System.Threading.ThreadStart method)
		{
			var watch = Stopwatch.StartNew();
			watch.Start();
			method.Invoke();
			watch.Stop();
			Info("{0} = {1}ms", title, watch.ElapsedMilliseconds);
		}

        string GetPrefix(string prf)
        {
            return string.Format("{0:yyyyMMdd}|{0:HH}H{0:mm}:{0:ss}.{0:ffff}\t|{1}\t| :", DateTime.Now, prf);
        }

		void SendMessage(string prefix, string body, params object[] prms)
		{
			SendMessage(prefix, string.Format(body, prms));
		}

		void SendMessage(string prefix, string body)
		{
			var message = new MailMessage();
			message.Body = body;
			message.From = new MailAddress("exception@erpstore.net");
			message.To.Add(new MailAddress("exception@erpstore.net"));
			message.Subject = string.Format("ERPStore.{0}", prefix);
			message.IsBodyHtml = false;

			var client = new SmtpClient();
			System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((object sender) =>
				{
					try
					{
						client.Send(message);
					}
					catch
					{
					}
				}));
		}

	}
}
