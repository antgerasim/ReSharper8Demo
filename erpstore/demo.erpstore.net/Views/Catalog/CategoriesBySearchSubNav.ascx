<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>

<h2 class="titleh2"><span>Affiner par Categorie</span></h2>
<div id="menu">
    <ul class="menu sf-menu">
    <% foreach (var item in Model.OrderByDescending(i => i.DeepProductCount).ThenBy(i => i.Name)) { %>
        <li class="navitem mn1navitem<%=item.Id%>" >
			<% if (item.Children.IsNotNullOrEmpty()) { %>
            <a class="parent" href="#">
                <span class="parent" style="background:url(<%=Url.ImageSrc(item,0,"") %>) no-repeat 2px 2px;">
					<%=item.Name%> (<%=item.DeepProductCount%>)
                </span>
             </a>
				<% if (item.Children.Count != 0) { %>
                <%Html.RenderPartial("categoriesbysearchsubsubnav", item.Children);%>
                <% } %>
            <% } else {%>
             <a href="<%=Url.AddParameter("category", item.Code) %>">
                <span><%=item.Name%> (<%=item.DeepProductCount%>)</span>
             </a>   
            <% } %>
        </li>
    <% } %>
    </ul>
</div>


