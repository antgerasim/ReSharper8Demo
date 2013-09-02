using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore
{
	public static class TransactionHelper
	{
		public static System.Transactions.TransactionScope GetNewReadCommitted()
		{
			return new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.RequiresNew
							, new System.Transactions.TransactionOptions()
							{
								IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
							});
		}
	}
}
