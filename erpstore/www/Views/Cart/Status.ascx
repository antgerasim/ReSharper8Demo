<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Cart>" %>
<% if (Model == null || Model.ItemCount == 0)
   { %>
<img src="/content/images/logo_esp_panier.jpg" alt="votre panier"/>
<p><span><%=Html.CartLink("Your cart")%></span></p>
<p id="cartcount">is empty</p>
<p id="carttotal"></p>
<% } 
   else if (Model.ItemCount == 1)
   { %>
<img src="/content/images/logo_esp_panier1.gif" alt="your cart"/>
<p><span><%=Html.CartLink("Your cart")%></span></p>
<p class="headerprix_quantite" id="cartcount"><b>1</b>product</p>
<p class="headerprix_prix"id="carttotal"><%=Model.TotalWithTax.ToString("#,##0.00")%> <small>€</small></p>
<% }
   else
   { %>
<a href="<%=Url.CartHref()%>" title="your cart"><img src="/content/images/logo_esp_panier1.gif" alt="votre panier"/></a>
<p><span><%=Html.CartLink("Your cart")%></span></p>
<p class="headerprix_quantite" id="cartcount"><%=string.Format("{0} products", Model.ItemCount)%></p>
<p class="headerprix_prix" "id="carttotal"><b><%=Model.TotalWithTax.ToString("#,##0.00")%></b><small>€</small></p>
<% } %>

