using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace ERPStore.Logging
{
	public class UDPLogger : ILogger, IDisposable
	{
		UdpClient m_Sock;
		IPEndPoint m_Iep;

		public UDPLogger(int port)
		{
			m_Sock = new UdpClient();
			m_Iep = new IPEndPoint(IPAddress.Broadcast, port);
		}

		#region ILogger Members

		public void Info(string message)
		{
			message = GetPrefix("Info") + message;
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Info(string message, params object[] prms)
		{
			message = GetPrefix("Info") + string.Format(message, prms);
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Notification(string message)
		{
			message = GetPrefix("Notification") + message;
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Notification(string message, params object[] prms)
		{
			message = GetPrefix("Notification") + string.Format(message, prms);
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Warn(string message)
		{
			message = GetPrefix("Warn") + message;
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Warn(string message, params object[] prms)
		{
			message = GetPrefix("Warn") + string.Format(message, prms);
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Debug(string message)
		{
			message = GetPrefix("Debug") + message;
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Debug(string message, params object[] prms)
		{
			message = GetPrefix("Debug") + string.Format(message, prms);
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Error(string message)
		{
			message = GetPrefix("Error") + message;
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Error(string message, params object[] prms)
		{
			message = GetPrefix("Error") + string.Format(message, prms);
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Error(Exception x)
		{
			var message = GetPrefix("Error") + x.ToString();
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Fatal(string message)
		{
			message = GetPrefix("Fatal") + message;
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Fatal(string message, params object[] prms)
		{
			message = GetPrefix("Fatal") + string.Format(message, prms);
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public void Fatal(Exception x)
		{
			var message = GetPrefix("Fatal") + x.ToString();
			byte[] data = Encoding.UTF8.GetBytes(message);
			m_Sock.Send(data, data.Length, m_Iep);
		}

		public System.IO.TextWriter Out
		{
			get 
			{ 
				return null; 
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

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			m_Sock.Close();
			m_Iep = null;
			m_Sock = null;
		}

		#endregion

		string GetPrefix(string prf)
		{
			var time = "{0:yyyyMMdd}/{0:HH}H{0:mm}:{0:ss}.{0:ffff}";
			return string.Format(time + "|{1}|", DateTime.Now, prf);
		}

	}
}
