<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<QuoteList>" %>
 
        <ul class="onglets">
            <li><a href="<%=Url.AddParameter("StatusId","0")%>" class="corner">Tous les devis</a></li>
            <li><a href="<%=Url.AddParameter("StatusId","1")%>" class="corner">Les devis en cours</a></li>
            <li><a href="<%=Url.AddParameter("StatusId","4")%>" class="corner">Les devis convertis en commande</a></li>
        </ul> 

        <table class="nav cols">
            
                <thead>
                    <tr class="entete-cols">
                        <th class="col col-25 col1">Code</th>
                        <th class="col col-20 col2">Status</th>
                        <th class="col col-25 col3">Création</th>
                        <th class="col col-15 col4">Nb. de produits</th>
                        <th class="col col-15 col5">Total HT</th> 
                    </tr>
                <thead>
 
                <tbody>
				<%foreach (var quote in Model) { %>
                 <tr class="<%=Model.ColumnIndexName(quote, 2, "prodligne")%>">
                    <td class="col col-25 col1">
						Code : <a href="<%=Url.Href(quote)%>" title="voir le détail"><%=quote.Code%></a></strong><br/><small>(ref : <%=quote.CustomerDocumentReference%>) </small>
                    </td>        
                    <td class="col col-20 col2">
                        <%=quote.Status.ToLocalizedName() %>
                    </td>
                    <td class="col col-25 col3 cold">
                        <%=string.Format("{0:dddd dd MMMM yyyy}", quote.CreationDate)%>
                    </td>
                    <td class="col col-15 col4 form_element_radio">
                         <%=quote.ItemCount%>
                    </td>
                    <td class="col col-15 col5 cold">
						<%=quote.GrandTotal.ToCurrency() %>
                    </td>
              </tr>
            <% } %>
        </tbody>
        
   </table> 
<script type="application/javascript">
    $(document).ready(function(){
         $(".prod").textHighlight();
            });
 </script>    