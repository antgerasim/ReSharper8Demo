using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ERPStore.Logging
{
	public class DiagnosticsLogger : ILogger
	{
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
			// message = GetPrefix("Debug") + message;
            System.Diagnostics.Debug.WriteLine(message, "Debug");
		}

		public void Debug(string message, params object[] prms)
		{
			// message = GetPrefix("Debug") + string.Format(message, prms);
            System.Diagnostics.Debug.WriteLine(string.Format(message, prms));
		}

		public void Error(string message)
		{
			// message = GetPrefix("Error") + message;
            System.Diagnostics.Trace.TraceError(message, "Error");
		}

		public void Error(string message, params object[] prms)
		{
			// message = GetPrefix("Error") + string.Format(message, prms);
            System.Diagnostics.Trace.TraceError(message, prms);
		}

		public void Error(Exception x)
		{
			// string message = GetPrefix("Error") + x.ToString();
            System.Diagnostics.Trace.TraceError(x.ToString());
		}

		public void Fatal(string message)
		{
			// message = GetPrefix("Fatal") + message;
            System.Diagnostics.Trace.TraceError(message);
		}

		public void Fatal(string message, params object[] prms)
		{
			message = GetPrefix("Fatal") + message;
            System.Diagnostics.Trace.TraceError(message, prms);
		}

		public void Fatal(Exception x)
		{
			string message = GetPrefix("Fatal") + x.ToString();
            System.Diagnostics.Trace.TraceError(message);
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
            var thread = System.Threading.Thread.CurrentThread;
            return string.Format("{0:yyyyMMdd}|{0:HH}H{0:mm}:{0:ss}.{0:ffff}\t|{1}\t|\t{2} :", DateTime.Now, prf, thread.Name);
        }
	}
}
