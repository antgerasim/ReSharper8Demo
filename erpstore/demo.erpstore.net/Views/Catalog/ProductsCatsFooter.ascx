<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<% foreach(var item in ViewData.Model) { %>
	<li id="footernav11"><a href="<%=Url.Href(item)%>" title="<%=item.Title%>"><% =item.Title %></a></li>
<% } %>