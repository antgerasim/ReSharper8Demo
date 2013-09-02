<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<User>" %>
<h2 class="titleh2"><span>Informations compte</span></h2>
<div class="menu2 menu_compte">
    <ul class="menu">
		<% if (Model != null) { %>
        <li class="navitem navcpte li_mnu mndevis"><a href="<%=Url.QuoteListHref()%>"><span>Devis</span></a></li>
        <li class="navitem navcpte li_mnu mncommandes"><a href="<%=Url.OrderListHref()%>"><span>Commandes</span></a></li>
        <li class="navitem navcpte li_mnu mnfactures"><a href="<%=Url.InvoiceListHref()%>"><span>Factures</span></a></li>
        <li class="navitem navcpte"><a href="<%=Url.EditUserHref()%>"><span>Informations personnelles</span></a></li>
        <li class="navitem navcpte"><a href="<%=Url.EditAddressListHref()%>"><span>Adresses</span></a></li>
        <% if (Model.Corporate != null) { %>
        <li class="navitem navcpte"><a href="<%=Url.EditCorporateHref()%>"><span>Informations Société</span></a></li>
        <% } %>
       <li class="navitem navcpte"><a href="<%=Url.CustomerProductListHref()%>"><span>Les produits déja commandés</span></a></li>
        <% } %>
       <li class="navitem" id="mn2navitem1"><a href="<%=Url.DestockProductListHref()%>" title="déstockage"><span>Déstockage</span></a></li>
       <li class="navitem" id="mn2navitem2"><a href="<%=Url.TopSellsProductListHref()%>" title="top ventes"><span>Top ventes</span></a></li>
       <li class="navitem" id="mn2navitem6"><a href="<%=Url.NewProductsListHref()%>" title="nouveautés"><span>Nouveautés</span></a></li>
       <li class="navitem lastitem" id="mn2navitem5"><a href="<%=Url.BrandsHref()%>"><span>Marques</span></a></li>
     </ul>
</div>
