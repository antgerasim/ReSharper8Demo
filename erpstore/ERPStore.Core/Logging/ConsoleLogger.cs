using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ERPStore.Logging
{
	public class ConsoleLogger : ILogger
	{

		public ConsoleLogger()
		{
		}

		public System.Web.HttpContext HttpContext
		{
			get
			{
				return System.Web.HttpContext.Current;
			}
		}


		public void Info(string message)
		{
			message = GetPrefix("Info") + message;
            // Console.WriteLine(message);
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Write(message);
			}
		}

		public void Info(string message, params object[] prms)
		{
			message = GetPrefix("Info") + message;
			// Console.WriteLine(message, prms);
			System.Diagnostics.Trace.WriteLine(string.Format( message, prms));
			if (HttpContext != null)
			{
				HttpContext.Trace.Write(string.Format(message, prms));
			}
		}

		public void Notification(string message)
		{
			message = GetPrefix("Info") + message;
			// Console.WriteLine(message);
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Write(message);
			}
		}

		public void Notification(string message, params object[] prms)
		{
			message = GetPrefix("Info") + message;
			// Console.WriteLine(message, prms);
			System.Diagnostics.Trace.WriteLine(string.Format(message, prms));
			if (HttpContext != null)
			{
				HttpContext.Trace.Write(string.Format(message, prms));
			}
		}

		public void Warn(string message)
		{
			message = GetPrefix("Warn") + message;
			// Console.WriteLine(message);
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(message);
			}
		}

		public void Warn(string message, params object[] prms)
		{
			message = GetPrefix("Warn") + message;
			// Console.WriteLine(message, prms);
			System.Diagnostics.Trace.WriteLine(string.Format(message, prms));
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(string.Format(message, prms));
			}
		}

		public void Debug(string message)
		{
			message = GetPrefix("Debug") + message;
			// Console.WriteLine(message, "Debug");
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Write(message);
			}
		}

		public void Debug(string message, params object[] prms)
		{
			message = GetPrefix("Debug") + string.Format(message, prms);
			// Console.WriteLine(string.Format(message, prms));
			System.Diagnostics.Trace.WriteLine(string.Format(message, prms));
			if (HttpContext != null)
			{
				HttpContext.Trace.Write(string.Format(message, prms));
			}

		}

		public void Error(string message)
		{
			message = GetPrefix("Error") + message;
			// Console.WriteLine(message, "Error");
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(message);
			}
		}

		public void Error(string message, params object[] prms)
		{
			message = GetPrefix("Error") + string.Format(message, prms);
			// Console.WriteLine(message, prms);
			System.Diagnostics.Trace.WriteLine(string.Format(message, prms));
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(string.Format(message, prms));
			}
		}

		public void Error(Exception x)
		{
			string message = GetPrefix("Error") + x.ToString();
			// Console.WriteLine(x.ToString());
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(message);
			}
		}

		public void Fatal(string message)
		{
			message = GetPrefix("Fatal") + message;
			// Console.WriteLine(message);
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(message);
			}
		}

		public void Fatal(string message, params object[] prms)
		{
			message = GetPrefix("Fatal") + message;
			// Console.WriteLine(message, prms);
			System.Diagnostics.Trace.WriteLine(string.Format(message, prms));
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(string.Format(message, prms));
			}
		}

		public void Fatal(Exception x)
		{
			string message = GetPrefix("Fatal") + x.ToString();
			// Console.WriteLine(message);
			System.Diagnostics.Trace.WriteLine(message);
			if (HttpContext != null)
			{
				HttpContext.Trace.Warn(message);
			}
		}

		private ConsoleTextWriter m_Out;

		public System.IO.TextWriter Out
		{
			get
			{
				if (m_Out == null)
				{
					m_Out = new ConsoleTextWriter();
				}
				return m_Out;
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
            var thread = System.Threading.Thread.CurrentThread;
            return string.Format("{0:yyyyMMdd}|{0:HH}H{0:mm}:{0:ss}.{0:ffff}\t|{1}\t|\t{2} :", DateTime.Now, prf, thread.Name);
        }
	}
}
