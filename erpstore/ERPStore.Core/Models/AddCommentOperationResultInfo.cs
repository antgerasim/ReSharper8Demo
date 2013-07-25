using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	public class AddCommentOperationResultInfo : AjaxOperationResultInfo
	{
		public string CommentDate { get; set; }
		public string CommentMessage { get; set; }
	}
}
