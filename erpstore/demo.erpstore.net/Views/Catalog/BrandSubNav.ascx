<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Brand>>" %>
<% if (Model.Count != 0) { %>
<div class="bloc bloc_navbrand">  
    <h2 class="titleh2"><span>Affiner par Marque</span></h2>
    <ul>
    <% foreach (var item in Model) { %>
		<li>
        	<a href='<%=Url.AddParameter("brand", item.Name)%>' title="Marque : <%=item.Name%>"><span><%=item.Name%>&nbsp;(<%=item.ProductCount%>)</span></a>
        </li>
	<% } %>
    </ul>
</div>
<%}%>
 


