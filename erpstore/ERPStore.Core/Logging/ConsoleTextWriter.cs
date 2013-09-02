using System;
using System.Collections.Generic;
using System.Text;

namespace ERPStore.Logging
{
	public class ConsoleTextWriter : System.IO.TextWriter
	{
		public override void Write(string value)
		{
			// Console.WriteLine(value);
			System.Diagnostics.Trace.WriteLine(value);
		}

		public override void Write(string format, object args)
		{
			// Console.WriteLine(format, args);
			System.Diagnostics.Trace.WriteLine(string.Format(format, args));
		}

		public override void WriteLine(string value)
		{
			// Console.WriteLine(value);
			System.Diagnostics.Trace.WriteLine(value);
		}

		public override Encoding Encoding
		{
			get { return Encoding.UTF8; }
		}
	}
}
