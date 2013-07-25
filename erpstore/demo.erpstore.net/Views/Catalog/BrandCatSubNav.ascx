<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>
<% if (Model.IsNotNullOrEmpty()) { %>
<div class="bloc bloc_navcat">  
    <h2 class="titleh2"><span>affiner par marques</span></h2>
    <ul>
<% foreach (var item in Model.OrderByDescending(i => i.DeepProductCount).ThenBy(i => i.Name)) { %>
	<li><a href='<%=Url.AddParameter("category", item.Code)%>' title='Categorie : <%=item.Name%>'><span><%=item.Name%>&nbsp;(<%=item.DeepProductCount%>)</span></a></li>
		<% if (item.Children.Count != 0) { %>
		<ul>
		<% foreach (var subitem in item.Children) { %>
			<li><a href='<%=Url.AddParameter("category", subitem.Code)%>' title='Categorie : <%=subitem.Name%>'><span><%=subitem.Name%>&nbsp;(<%=subitem.DeepProductCount%>)</span></a></li>
		<% } %>
		</ul>
	<% } %>
<% } %>
</ul>
</div>
<%}%>
