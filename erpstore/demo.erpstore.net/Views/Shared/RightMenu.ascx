<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="menu2">
    <ul class="menu">
       <li class="navitem" id="mn2navitem1"><a href="<%=Url.DestockProductListHref()%>" title="d�stockage"><span>D�stockage</span></a></li>
       <li class="navitem" id="mn2navitem2"><a href="<%=Url.TopSellsProductListHref()%>" title="top ventes"><span>Top ventes</span></a></li>
       <li class="navitem" id="mn2navitem3"><a href="<%=Url.PromotionsProductListHref() %>" title="Promotions"><span>Promotions</span></a></li>
       <li class="navitem" id="mn2navitem6"><a href="<%=Url.NewProductsListHref()%>" title="nouveaut�s"><span>Nouveaut�s</span></a></li>
<% if (Request.IsAuthenticated) { %>
        <li class="navitem" id="mn2navitem4"><a href="<%=Url.CustomerProductListHref() %>" title="La liste de tous les produits d�j� command�s"><span>Mes Produits</span></a></li>
		<li class="navitem" id="mn2navitem5"><a href="<%=Url.BrandsHref()%>" title="Toutes les marques"><span>Marques</span></a></li>
        <li class="navitem itemconnect"  id="mn2navitem7"><a href="<%=Url.QuoteListHref() %>" title="La liste des devis"><span>Mes Devis</span></a></li>
        <li class="navitem itemconnect"  id="mn2navitem8"><a href="<%=Url.OrderListHref() %>" title="La liste des commandes"><span>Mes Commandes</span></a></li>
    	<li class="navitem itemconnect" id="mn2navitem9"><a href="<%=Url.InvoiceListHref() %>" title="La liste des factures"><span>Mes Factures</span></a></li>
		<li class="navitem itemconnect" id="mn2navitem10"><a href="<%=Url.EditUserHref() %>" title="Edition des coordonn�es"><span>Mes Coordonn�es</span></a></li>
       <% if (this.ViewContext.HttpContext.User.GetUserPrincipal().CurrentUser.Corporate != null) { %>
        <li class="navitem itemconnect" id="mn2navitem11"><a href="<%=Url.EditCorporateHref() %>" title="Edition des la soci�t�"><span>Ma Soci�t�</span></a></li>
       <% } %> 
      <li class="navitem itemconnect lastitem" id="mn2navitem12"><a href="<%=Url.EditAddressListHref() %>" title="Edition des adresses"><span>Mes Adresses</span></a></li>
<% } else { %>
       <li class="navitem lastitem" id="mn2navitem5"><a href="<%=Url.BrandsHref()%>" title="Toutes les marques"><span>Marques</span></a></li>
<% } %>

      </ul>
</div>
