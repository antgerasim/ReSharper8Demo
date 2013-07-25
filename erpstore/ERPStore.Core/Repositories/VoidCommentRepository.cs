using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	public class VoidCommentRepository : ICommentRepository
	{
		#region ICommentRepository Members

		public IList<ERPStore.Models.Comment> GetCommentListByModelAndId(string modelName, int id)
		{
			return null;
		}

		#endregion
	}
}
