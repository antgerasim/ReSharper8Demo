<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<div id="grid" class="grid">
<% foreach (var item in Model) 
   {
	   Html.RenderPartial("~/views/catalog/ProductItem.ascx", item);
   } %>
</div>
