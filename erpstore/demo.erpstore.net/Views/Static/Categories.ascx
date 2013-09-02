<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>
<div id="menu">
    <ul class="menu sf-menu">
    <% foreach (var item in Model.OrderBy(i => i.FrontOrder.GetValueOrDefault(int.MaxValue)).ThenBy(i => i.Name)) { %>
        <% if (!item.IsForefront) continue; %>
        <li class="navitem mn1navitem<%=item.Id%>">
			<% if (item.Children.Where(i => i.IsForefront).IsNotNullOrEmpty()) { %>
				<a class="parent" href="#">
					<span class="parent" style="background:url(<%=Url.ImageSrc(item,0,"") %>) no-repeat 2px 2px;">
						<%=item.Name%>
					</span>
				 </a>
				<% if (item.Children.Count != 0) { %>
					<Html.RenderPartial("subcategories", item.Children);>
                <% } %>
            <% } else {%>
             <a href="<%=Url.Href(item)%>">
                <span><%=item.Name%></span>
             </a>   
            <% } %>
        </li>
    <% } %>
    </ul>
</div>
