<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<% if (Model.IsNotNullOrEmpty()) { %>
<h3><span>&nbsp;Avec ce produit, nous vous conseillons :</span></h3>
<div class="bloc bloc_home">
<% foreach (var product in Model.Take(3)) { %>
       <div class="prod">
       		<div class="prod-promo"></div>
            <a  name="<%=product.Code%>" class="prod_ancre"></a>
            <div class="prodcontent" id="prod<%=product.Code.GetHashCode()%>">
                <h3>
                    <a href="<%=Url.Href(product) %>">
						<%=product.Title %>
                    </a>
                </h3>
                <div class="prodinfos">
                    <%Html.ShowProductPrice(product);%>
                </div>
                <div class="prodimginfos">
                    <img src="<%=Url.ImageSrc(product, 140,140, "/content/images/vignette140.png")%>" alt="<%=product.Title %>"/>
                </div>
            </div>       		
       </div>
<% } %>
</div>
<% } %>