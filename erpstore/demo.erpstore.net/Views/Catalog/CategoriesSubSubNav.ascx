<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>
<ul class="subsubnavcat">
<% foreach (var item in Model) { %>
   	<li><a href="<%=Url.Href(item) %>"><span><%=item.Name %> (<%=item.DeepProductCount%>)</span></a></li>
    <% if (item.Children.Count != 0) { %>
		<%Html.RenderPartial("CategoriesSubSubNav", item.Children);%>
     <%}%>
<% } %>
</ul>