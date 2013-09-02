using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public class ConsoleTextWriter : System.IO.TextWriter
	{
		public override void Write(string value)
		{
			Console.WriteLine(value);
		}

		public override void Write(string format, object arg)
		{
			Console.WriteLine(format, arg);
		}

		public override void WriteLine(string value)
		{
			Console.WriteLine(value);
		}

		public override Encoding Encoding
		{
			get { return Encoding.UTF8; }
		}
	}
}
