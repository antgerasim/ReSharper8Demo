<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<QuoteCart>" %>
<% if (Model == null || Model.ItemCount == 0) { %>
<div id="quotecartstatus"  class="statusvide">
<p><span><%=Html.QuoteCartLink("Votre demande de prix")%></span></p>
<p id="quotecartcount">est vide
<br/>
<img src="/content/images/caddie_vide.jpg" alt="Votre panier est vide" align="absmiddle"/>
</p>
</div>
<% } else if (Model.ItemCount == 1) { %>
<div id="quotecartstatus" class="status1">
<p><span><%=Html.QuoteCartLink("Votre demande de prix")%></span></p>
<p class="headerprix_quantite" id="quotecartcount"><b>1</b>produit</p>
</div>
<% } else { %>
<div id="quotecartstatus"  class="statusplusieurs">
<p><span><%=Html.QuoteCartLink("Votre demande de prix")%></span></p>
<p class="headerprix_quantite" id="quotecartcount"><%=string.Format("<b>{0} produits</b>", Model.ItemCount)%></p>
</div>
<% } %>
