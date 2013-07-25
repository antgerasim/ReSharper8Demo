<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>

<ul class="bottomnav" id="bottomnav0">
    <li class="active"><a href="/" title="">Accueil</a></li>
<% foreach (var item in Model) { %>
	<li class="navitem"><%=Html.ProductCategoryLink(item)%> 
    </li>
<% } %>
</ul>

 


