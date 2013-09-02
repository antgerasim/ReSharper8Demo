using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models.Security
{
	internal class LocalizeContentCryptoParameter : CrypoParameterBase
	{
		public string Path { get; set; }
		public string Key { get; set; }
	}
}
