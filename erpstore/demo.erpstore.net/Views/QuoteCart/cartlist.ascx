<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<QuoteCart>>"%>
<div class="bloc texte">
	<div class="bloc_type_liste">
        <h2 class="titleh2"><span>Liste des devis en cours</span></h2>
        <ul class="bloc_paniers">
            <%foreach (var cart in Model) { %>
            <li>
                <span><b>Devis n°<%=Model.IndexOf(cart) + 1%></b><small> créé le  <%=cart.CreationDate.ToString("dd/MM/yyyy")%></small></span>
                 <span><%=cart.ItemCount%> produit(s)</span>
                <br/>
                <% if (!cart.IsActive) { %>
                <a href="<%=Url.ChangeQuoteCartHref(cart.Code)%>" title="Sélectionner ce devis">Sélectionner</a>
				<% } %>
                <a href="<%=Url.ShowQuoteCartHref(cart.Code)%>" title="Partager ce devis avec d'autres personne (recopier ce lien)">Partager</a>
                <a href="<%=Url.DeleteQuoteCartHref(cart.Code)%>" title="supprimer ce devis">Supprimer</a>
            </li>
        <% } %>
         </ul>
         <ul class="bloc_bottom">
            <li><span><a href="<%=Url.CreateNewCartHref()%>">Créer un nouveau devis</a></span></li>
        </ul>
    </div>
</div>

