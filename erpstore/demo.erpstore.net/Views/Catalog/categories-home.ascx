<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ProductCategory>>" %>
<% foreach (var item in Model) { %>
<!-- produit //-->
<div class="prod corner" id="">
    <div class="prodcontent">
        <% Html.ShowHeadProductOfCategory(item); %>
    </div>
</div>
<!-- fin produit //--> 
<% } %>
