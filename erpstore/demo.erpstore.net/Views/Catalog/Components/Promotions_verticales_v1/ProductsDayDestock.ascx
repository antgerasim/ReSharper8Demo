<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<% if (Model.IsNotNullOrEmpty()) { %>
<h2 class="titleh2"><a href="<%=Url.RouteERPStoreUrl("Promotions", null) %>" title="Toutes les promotions"><span>Destockage</span></a></h2>
<div class="bloc bloc_home bloc_destock">
	<% foreach(var item in ViewData.Model) { %>
    <div class="prod prodtype<% =item.Character%> prodligne<%=Model.ColumnIndex(item,2)%>">
    	<div class="prod-promo"></div>
        <div class="prodcontent">
            <h3>
                <a href="<%=Url.Href(item)%>" title="<%=Html.Encode(item.Title)%>"><%=item.Title.EllipsisAt(20)%></a>
            </h3> 
            <div class="prodinfos">
                <%Html.ShowProductPrice(item);%>
            </div> 
            <div class="prodimginfos">
                   <a href="<%=Url.Href(item)%>" title="<%=Html.Encode(item.Title)%>"><% =Html.ProductImage(item, 140,140, "/content/images/vignette140.png")%></a>
            </div>
       </div>
     </div>
    <% } %>
</div>
<% } %>
