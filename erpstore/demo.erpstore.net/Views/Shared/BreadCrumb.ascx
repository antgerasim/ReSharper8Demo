<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dictionary<string,string>>" %>
<div class="chemin">
<%foreach(KeyValuePair<string, string> item in Model) { %>
<% if (Model.Last().Key != item.Key) { %>
    <span>
    	<a href="<%=item.Value%>"><%=item.Key%></a>
    </span>
	<%}%>
    <%else {%>
    <b><%=item.Key%></b>
    <%}%>
<%} %>
</div>
