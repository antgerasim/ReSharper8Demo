<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<ProductCategory>>" %>
<ul class="f12">
<% foreach (var item in Model) { %>
	<li><a href="<%=Url.Href(item)%>" ><%=item.Name%><img src="defaultIcon.jpg"></a></li>
<% } %>
</ul>

