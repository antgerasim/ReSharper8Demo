<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<OrderCart>>"%>
<div class="bloc texte">
	<div class="bloc_type_liste">
        <h2 class="titleh2"><span>Liste des paniers en cours</span></h2>
        <ul class="bloc_paniers">
			 <%foreach (var cart in Model) { %>
                <li>
                    <span><b>Panier n°<%=Model.IndexOf(cart) + 1%></b><small> créé le  <%=cart.CreationDate.ToString("dd/MM/yyyy")%></small></span>
                    <span><%=cart.ItemCount%> produit(s) - <strong><%=cart.Total.ToCurrency()%></strong></span>
                    <br/>
                    <% if (!cart.IsActive) { %>
                    <a href="<%=Url.ChangeCartHref(cart.Code)%>" title="Sélectionner ce panier">Sélectionner</a>
                    <% } %>
                    <a href="<%=Url.ShowCartHref(cart.Code)%>" title="Partager ce panier avec d'autre personnes qui pourront le visualiser (recopier ce lien)">Partager</a>
                    <a href="<%=Url.DeleteCartHref(cart.Code)%>" title="supprimer ce panier">Supprimer</a>
                </li>
        <% } %>
        </ul>
         <ul class="bloc_bottom">
         	<li>
            	<span><a href="<%=Url.CreateNewCartHref()%>">Créer un nouveau panier</a></span>
            </li>
        </ul>
    </div>
</div>

