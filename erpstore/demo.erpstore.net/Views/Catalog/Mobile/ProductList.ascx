<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<table width="100%">
<% foreach (var product in ViewData.Model) { %>
	<tr>
		<td width="30"><img src='<%=Url.ImageSrc(product, 30, "") %>' alt="" /></td>
		<td><%=product.Title%></td>
	</tr>
<%  } %>
</table>
