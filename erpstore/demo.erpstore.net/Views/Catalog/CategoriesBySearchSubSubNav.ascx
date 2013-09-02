<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>


<ul class="subnav">
<% foreach (var subitem in Model.OrderByDescending(i => i.DeepProductCount).OrderBy(i => i.Name)) { %>
    <li class="subnavitem subnav2 ">
	<% if (subitem.Children.IsNotNullOrEmpty()) { %>
        <a class="sublast parent" href="<%=Url.AddParameter("category", subitem.Code) %>">
			<span><%=subitem.Name%> (<%=subitem.DeepProductCount%>)</span>
         </a>
		<% Html.RenderPartial("categoriesbysearchsubsubnav", subitem.Children);%>
		<% } else {%>
         <a href="<%=Url.AddParameter("category", subitem.Code) %>">
			<span><%=subitem.Name%> (<%=subitem.DeepProductCount%>)</span>
         </a>
    <% } %>
    </li>  

<% } %>
</ul>