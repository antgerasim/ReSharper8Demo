<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ProductCategory>>" %>
<script type="text/javascript">
       $(function() {
		// rollover
		$(".subnavcat").accordion({
			event: "mouseover",
			autoHeight: false,
			active: false
		});
       });
</script>
<% if (Model.Count != 0) { %>
<div class="bloc bloc_navcat"> 
    <h2 class="titleh2"><span>Affiner par catégories</span></h2>
    <div class="subnavcat">
    <% foreach (var item in Model) { %>
        <% if (item.Children.Count != 0) { %>
        <ul>
        	<li>
            <a href="<%=Url.Href(item)%>"><span><%=item.Name %> <small>(<%=item.DeepProductCount%>)</small></span></a>
            </li>
        </ul>
        <%Html.RenderPartial("CategoriesSubSubNav", item.Children);%>
        <%}%>
    <% } %>
    </div>
</div>
<%}%>

 


