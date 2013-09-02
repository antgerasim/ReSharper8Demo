<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>
<ul class="subnav">
<% foreach (var subitem in Model.OrderBy(i => i.FrontOrder.GetValueOrDefault(int.MaxValue)).ThenBy(i => i.Name)) { %>
<% if (!subitem.IsForefront) continue; %>
   	
    <li class="subnavitem subnav2 ">
	<% if (subitem.Children.IsNotNullOrEmpty()) { %>
        <a class="sublast parent" href="<%=Url.Href(subitem)%>">
			<span><%=subitem.Name%></span>
         </a>
		<% Html.RenderPartial("subcategories", subitem.Children);%>
		<% } else {%>
         <a href="<%=Url.Href(subitem)%>">
			<span><%=subitem.Name%></span>
         </a>
    <% } %>
    </li>  

<% } %>
</ul>
