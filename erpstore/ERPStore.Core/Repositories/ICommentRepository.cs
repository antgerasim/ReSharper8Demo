using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	public interface ICommentRepository
	{
		IList<Models.Comment> GetCommentListByModelAndId(string modelName, int id);
	}
}
