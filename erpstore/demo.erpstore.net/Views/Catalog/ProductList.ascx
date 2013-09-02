<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<div class="bloc bloc_home bloc_pages">
    <% foreach (var item in ViewData.Model)
       { %>
       <div class="prod prodtype<% =item.Character%> <%=Model.ColumnIndexName(item, 2, "prodligne")%>">
       		<div class="prod-promo"></div>
       		<% Html.RenderPartial("ProductInfo", item); %>
       </div>
    <%  } %>
</div>