<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<UserDashboard>" %>
<div class="compte-list">
     <div class="bloc_type_prod">
          <h4 class="h4-devis">Devis</h4>
          <p>Devis non validés</p>
          <ul class="ul_list">
			 <% foreach (var item in Model.WaitingQuoteList) { %>
                 <li>
                     <strong>Devis N°<%=item.Code%></strong><small> (ref : <%=item.CustomerDocumentReference%>) <br/>du <%=string.Format("{0:dd/MM/yyyy}", item.CreationDate)%></small><br />
                     <b><%=item.ItemCount%></b> produit(s) <small>pour un montant de</small> <%=item.GrandTotal.ToCurrency()%><br />
                     <a href="<%=Url.Href(item)%>">Voir le détail</a>
                </li>
            <% } %>
         </ul>
         <ul class="bloc_bottom">
         	<li>
            <span><a href="<%=Url.QuoteListHref() %>">voir les devis</a></span>
            </li>
        </ul>
    </div>
    <div class="bloc_type_prod">
           <h4 class="h4-commandes">Commandes</h4>
           <p>
              Les dernieres commandes
            </p>
            <ul class="ul_list">
            	<li> aucune
                </li>
            </ul>
        <ul class="bloc_bottom">
         	<li>
            <span><a href="#">voir les commandes</a></span>
            </li>
        </ul>
    </div>
    <div class="bloc_type_prod">
        <h4 class="h4-factures">Factures</h4>
           <p>
              Les factures en cours
            </p>
            <ul class="ul_list">
            	<li> aucune
                </li>
            </ul>
        <ul class="bloc_bottom">
         	<li>
            <span><a href="#">Voir les factures</a></span>
            </li>
        </ul>
    </div>
</div>
<br class="clear" />


