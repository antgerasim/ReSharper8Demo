<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<ERPStore.Models.MenuItem>>" %>
<div id="navigation">
    <ul class="menu">
    <% foreach (var item in Model) { %>
		<li><a href="<%=Url.Href(item)%>" ><%=item.RouteName%><img src="defaultIcon.jpg"></a></li>
	<% } %>
    </ul>
</div>


