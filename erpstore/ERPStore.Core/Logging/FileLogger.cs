using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Timers;

namespace ERPStore.Logging
{
	public class FileLogger : ILogger, IDisposable
	{
		private static object m_Lock = new object();
		private Timer m_Timer;
		private string m_FileName;
		private DateTime m_LastCleanerTime;
		protected bool m_IsDebugMode = false;

		public FileLogger()
		{
			m_FileName = GetLogFileName();
			m_Timer = new Timer(1000 * 60); // Toutes les 60 secondes
			m_Timer.Elapsed += (s, arg) =>
			{
				m_FileName = GetLogFileName();
				CleanLogFiles();
			};
			m_Timer.Start();
			m_LastCleanerTime = DateTime.Now;

			var compilationSection = (System.Web.Configuration.CompilationSection)System.Configuration.ConfigurationManager.GetSection("system.web/compilation");
			m_IsDebugMode = compilationSection.Debug;
		}

		~FileLogger()
		{
			Dispose();
		}

		protected virtual string GetLogFileName()
		{
			string fileName = null;
			string siteName = (global::ERPStore.ERPStoreApplication.WebSiteSettings != null) ? global::ERPStore.ERPStoreApplication.WebSiteSettings.SiteName : Guid.NewGuid().ToString();
			fileName = string.Format("erpstore.{0}.{1:yyyyMMddHH}.log", siteName, DateTime.Now);
			string logPath = Configuration.ConfigurationSettings.AppSettings["logPath"]
								?? System.Configuration.ConfigurationManager.AppSettings["tempPath"]
								?? System.IO.Path.GetTempPath();

			fileName = System.IO.Path.Combine(logPath, fileName);
			return fileName;

			//string fileName = null;
			//string siteName = (ERPStoreApplication.WebSiteSettings != null) ? ERPStoreApplication.WebSiteSettings.SiteName : Guid.NewGuid().ToString();
			//fileName = string.Format("erpstore.{0}.{1:yyyyMMddHH}.log", siteName, DateTime.Now);
			//var tempPath = System.Configuration.ConfigurationManager.AppSettings["tempPath"];
			//if (tempPath == null)
			//{
			//    tempPath = System.IO.Path.GetTempPath();
			//}
			//fileName = System.IO.Path.Combine(tempPath, fileName);
			//return fileName;
		}

		/// <summary>
		/// Supression des fichiers de log supérieurs a 2 jours
		/// </summary>
		private void CleanLogFiles()
		{
			// Ne faire le traitement que toutes les heures
			if (m_LastCleanerTime.AddHours(1) < DateTime.Now)
			{
				return;
			}

			var fileList = from fileName in System.IO.Directory.GetFiles(System.IO.Path.GetTempPath(), "erpstore*.log", System.IO.SearchOption.AllDirectories)
						   let fileInfo = new System.IO.FileInfo(fileName)
						   where fileInfo.LastWriteTime.AddDays(2) < DateTime.Now
						   select fileName;

			foreach (var fileName in fileList)
			{
				try
				{
					System.IO.File.Delete(fileName);
				}
				catch { }
			}

			m_LastCleanerTime = DateTime.Now;
		}

		public virtual void Info(string message)
		{
			Write("Info",message);
		}

		public virtual void Info(string message, params object[] prms)
		{
			Write("Info", message, prms);
		}

		public virtual void Notification(string message)
		{
			Write("Notification", message);
		}

		public virtual void Notification(string message, params object[] prms)
		{
			Write("Notification", message, prms);
		}

		public virtual void Warn(string message)
		{
			Write("Warn", message);
		}

		public virtual void Warn(string message, params object[] prms)
		{
			Write("Warn", message, prms);
		}

		public virtual void Debug(string message)
		{
			if (m_IsDebugMode)
			{
				Write("Debug", message);
			}
		}

		public virtual void Debug(string message, params object[] prms)
		{
			if (m_IsDebugMode)
			{
				Write("Debug", message, prms);
			}
		}

		public virtual void Error(string message)
		{
			Write("Error", message);
		}

		public virtual void Error(string message, params object[] prms)
		{
			Write("Error", message, prms);
		}

		public virtual void Error(Exception x)
		{
			string message = GetExceptionContent(x,0);
			Write("Error", message);
		}

		public virtual void Fatal(string message)
		{
			Write("Fatal", message);
		}

		public virtual void Fatal(string message, params object[] prms)
		{
			Write("Fatal", message, prms);
		}

		public virtual void Fatal(Exception x)
		{
			string message = GetExceptionContent(x,0);
			Write("Fatal", message);
		}

		public static string GetExceptionContent(Exception ex, int level)
		{
			var content = new StringBuilder();
			content.Append("--------------------------------------------");
			content.AppendLine();
			content.AppendLine(ex.Message);
			content.AppendLine("--------------------------------------------");

			var sqlEx = ex as System.Data.SqlClient.SqlException;
			var key = string.Format("SqlDataExecption:{0}",level);
			if (sqlEx != null
				&& !ex.Data.Contains(key))
			{
				ex.Data.Add(key, "--------------------");
				ex.Data.Add(string.Format("SqlErrorCode:{0}",level), sqlEx.ErrorCode);
				ex.Data.Add(string.Format("LineNumber:{0}",level), sqlEx.LineNumber);
				ex.Data.Add(string.Format("Number:{0}",level), sqlEx.Number);
				ex.Data.Add(string.Format("Procedure:{0}",level), sqlEx.Procedure);
				ex.Data.Add(string.Format("Server:{0}", level), sqlEx.Server);
				// x.Data.Add("", sqlEx.State);
			}

			// Ajout des extensions d'erreur
			if (ex.Data != null 
				&& ex.Data.Count > 0)
			{
				foreach (var item in ex.Data.Keys)
				{
					if (item != null && ex.Data != null && ex.Data[item] != null)
					{
						string data = string.Empty;
						try
						{
							data = ex.Data[item].ToString();
							content.AppendFormat("{0} = {1}", item, data);
						}
						catch { }
					}
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
				content.Append(GetExceptionContent(ex.InnerException, level++));
			}
			return content.ToString();
		}

		public virtual System.IO.TextWriter Out
		{
			get
			{
				return new System.IO.StreamWriter(GetStream());
			}
		}

		public virtual void Watch(string title, System.Threading.ThreadStart method)
		{
			var watch = Stopwatch.StartNew();
			watch.Start();
			method.Invoke();
			watch.Stop();
			Info("{0} = {1}ms", title, watch.ElapsedMilliseconds);
		}

		void Write(string prefix, string body, params object[] prms)
		{
			try
			{
				Write(prefix, string.Format(body, prms));
			}
			catch { }
		}

		protected virtual System.IO.Stream GetStream()
		{
			return new System.IO.FileStream(m_FileName, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write);
		}

		void Write(string prefix, string body)
		{
			System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((object sender) =>
			{
				try
				{
					var line = string.Format("{0:yyyyMMdd}|{0:HH}H{0:mm}:{0:ss}.{0:ffff}|{1}|{2}\r\n", DateTime.Now, prefix, body);
					using (var s = new System.IO.FileStream(m_FileName, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write))
					{
						var b = System.Text.Encoding.UTF8.GetBytes(line);
						s.Write(b, 0, b.Length);
						s.Close();
					}
				}
				catch { }
			}));
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_Timer != null)
			{
				m_Timer.Close();
				m_Timer.Dispose();
			}
		}

		#endregion
	}
}
