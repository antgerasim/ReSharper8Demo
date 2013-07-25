<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MenuItem>>" %>
<ul class="nav" id="nav2">
<% foreach (var item in Model) { %>
	<li id="nav2<%=Model.IndexOf(item)+1%>"><a href="<%=Url.Href(item)%>"><%=item.Text%></a></li>
<% } %>
</ul>