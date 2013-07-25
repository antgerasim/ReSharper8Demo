<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<div class="bloc-list total-list" id="productlist">
    
    <ul class="nav entete ui-tabs-nav">
        <li id="entete-photo">photo</li>
        <li id="entete-category">catégories</li>
        <li id="entete-marque">marque</li>
        <li id="entete-produit">produit</li>
        <li id="entete-prix">prix</li>
        <li id="entete-description">description</li>
    </ul>
    <% foreach (var item in ViewData.Model)
       { %>
       <div class="prod prodtype<% =item.Character%>" <%=Model.ColumnIndexName(item, 2, "prodligne")%>">
       <% Html.RenderPartial("ProductTotalInfo", item); %>
       </div>
    <%  } %>
    
</div>