using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ERPStore.Configuration
{
	public class PaymentConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("payments", IsRequired = false)]
		public PaymentConfigurationElementCollection Payments
		{
			get
			{
				return (PaymentConfigurationElementCollection)this["payments"];
			}
			set
			{
				this["payments"] = value;
			}
		}

	}
}
