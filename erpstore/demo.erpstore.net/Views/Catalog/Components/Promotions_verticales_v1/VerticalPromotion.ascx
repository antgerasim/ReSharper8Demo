<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h2 class="titleh2"><span>promotions</span></h2>
<div class="bloc bloc_home bloc_promotions">
  <% Html.ShowProductList(ProductListType.Promotional, "Components/Promotions_verticales_v1/ProductsDayPromotion.ascx",3); %>
    <div class="prodbottom">
        <a href="<%=Url.RouteERPStoreUrl("Promotions", null) %>" title="Toutes les promotions"> Toutes les promotions</a>
    </div>   
</div>