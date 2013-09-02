<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OrderCart>" %>
<% if (Model == null || Model.ItemCount == 0) { %>
<div id="cartstatus"  class="statusvide">
<p><span><a href="<%=Url.CartHref()%>" title="votre panier">Votre panier</a></span></p>
<p id="cartcount">est vide
<br/>
<img src="/content/images/caddie_vide.jpg" alt="Votre panier est vide" align="absmiddle"/>
</p>
</div>
<% }  else if (Model.ItemCount == 1) { %>
<div id="cartstatus" class="status1">
<p><span><%=Html.CartLink("Votre panier")%></span></p>
<p class="headerprix_quantite" id="cartcount"><b>1</b>produit</p>
<p class="headerprix_prix" id="carttotal"><b><%=Model.Total.ToString("#,##0.00")%></b><small> € HT</small></p>
</div>
<% } else { %>
<div id="cartstatus" class="statuspluieurs">
<p><span><%=Html.CartLink("Votre panier")%></span></p>
<p class="headerprix_quantite" id="cartcount"><%=string.Format("<b>{0} produits</b>", Model.ItemCount)%></p>
<p class="headerprix_prix" id="carttotal"><b><%=Model.Total.ToString("#,##0.00")%></b><small> € HT</small></p>
</div>
<% } %>
