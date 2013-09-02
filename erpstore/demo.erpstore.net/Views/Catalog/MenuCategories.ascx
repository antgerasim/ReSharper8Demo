
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>
<div id="categories">
<h1>Categories</h1>
<% foreach (var item in ViewData.Model) { %>
	<h2><%=Html.ProductCategoryLink(item)%> </h2>
		<% foreach (var subitem in item.Children) { %>
			<%=Html.ProductCategoryLink(subitem)%>
			<% if (item.Children.IndexOf(subitem) != item.Children.Count - 1)
			{ %>,<% }} %>
		<% if (item.Children.Count > 0) {%>
		<br />
		<%} %>
<% } %>
</div>