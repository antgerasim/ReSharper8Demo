<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<InvoiceList>" %>
<h3>
 <a href="<%=Url.CurrentInvoiceListHref()%>"> Les factures en cours</a>
</h3>
<ul class="ul_list">
	 <% foreach (var item in Model) { %>
         <li>
             <strong>Facture N°<%=item.Code%></strong><small><br/>du <%=string.Format("{0:dd/MM/yyyy}", item.CreationDate)%></small><br />
             <b><%=item.ItemCount%></b> produit(s) <br/><small>pour un montant de</small> <%=item.GrandTotal.ToCurrency()%><br />
             <a href="<%=Url.Href(item)%>">Voir le détail</a>
        </li>
    <% } %>
</ul>