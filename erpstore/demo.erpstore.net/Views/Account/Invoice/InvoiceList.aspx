<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2Columns.Master" Inherits="System.Web.Mvc.ViewPage<InvoiceList>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Liste des factures</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div id="grid3">
	<div id="sgrid31">
		<%Html.RenderPartial("~/views/account/managemenu.ascx", User.GetUserPrincipal().CurrentUser);%>
         <div class="bloc texte">
            <div class="corner bloc_type_liste">
                <h2 class="titleh2"><span>Recherche</span></h2>
                <form method="get" action="<%=Request.Url.LocalPath%>" id="formFilter" >
                <p>
                    <input type="text" name="search" value='<%=Request["Search"]%>'/>
                    <input type="submit" value="Rechercher" id="submit" />
                </p>
                </form>
             </div>
         </div>
         
        <div class="bloc texte">
            <div class="corner bloc_type_liste">
                <h2 class="titleh2"><span>Période</span></h2>
                <p>
                <%=Html.ListBox("periodId", Html.GetPeriodFilterOptionList(), new { size = 15 })%>
                </p>
             </div>
         </div>
        
            <script type="text/javascript">
        
                $(document).ready(function() {
                    $('#periodId').change(function() {
                        url = this.options[this.selectedIndex].value;
                        location.href = url;
                    });
                });
                
            </script>

    </div>
	<div id="sgrid32">
     <h2 class="titleh2">
          <span>Votre compte personnalisé</span>
     </h2>
     <div class="chemin">
        <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
        <span><a href="<%=Url.AccountHref() %>">Tableau de bord</a></span>
        <span><a href="<%=Url.InvoiceListHref()%>">Factures</a></span>
        <b>
		<% if (Request["StatusId"] == "0") { %>
            <% if (Model.ItemCount == 0) { %>
            Il n'y a pas encore de facture
            <% } else { %> 
            Toutes les factures (<%=Model.ItemCount%>)
            <% } %> 
        <% } else if (Request["StatusId"] == "1") { %>
            <% if (Model.ItemCount == 0) { %>
            Il n'y a pas de facture en cours
            <% } else {  %> 
            Les <%=Model.ItemCount%> facture(s) en cours
            <% } %> 
        <% } else { %>
            <% if (Model.ItemCount == 0) { %>
            Il n'y a pas de facture en retard
            <% } else {  %> 
            Les <%=Model.ItemCount%> facture(s) en retard de reglement
            <% } %> 
        <% } %> 
        </b>
    </div>    

	<div class="ssgrid321">
    
	<%Html.ShowPager(Model); %>

        <ul class="onglets">
            <li><a href="<%=Url.AddParameter("StatusId","0")%>" class="corner">Toutes les factures</a></li>
            <li><a href="<%=Url.AddParameter("StatusId","1")%>" class="corner">Les factures en cours</a></li>
            <li><a href="<%=Url.AddParameter("StatusId","2")%>" class="corner">Les factures en retard</a></li>
        </ul> 
        
        <table class="nav cols">
            
                <thead>
                    <tr class="entete-cols">
                        <th class="col col-15 col1">Code</th>
                        <th class="col col-15 col2">Statut</th>
                        <th class="col col-15 col3">Création</th>
                        <th class="col col-15 col4">Réglement</th> 
                        <th class="col col-10 col5">Nb. de produits</th>
                        <th class="col col-15 col6">Total HT</th> 
                        <th class="col col-15 col7">Total TTC</th> 
                    </tr>
                </thead>
 
                <tbody>
				<%foreach (var invoice in Model) { %>
                 <tr class="<%=Model.ColumnIndexName(invoice, 2, "prodligne")%>">
                    <td class="col col-15 col1">
						Code : <a href="<%=Url.Href(invoice)%>" title="voir le détail"><%=invoice.Code%></a></strong>
                    </td>        
                    <td class="col col-15 col2">
                        <%=invoice.Status.ToLocalizedName() %>
                    </td>
                    <td class="col col-15 col3 cold">
                        <%=string.Format("{0:dddd dd MMMM yyyy}", invoice.CreationDate)%>
                    </td>
                    <td class="col col-15 col4 cold">
                    	<%=string.Format("{0:dddd dd MMMM yyyy}", invoice.ExpirationDate)%>
                    </td>
                    <td class="col col-10 col5 form_element_radio">
                         <%=invoice.ItemCount%>
                    </td>
                    <td class="col col-15 col5 cold">
						<%=invoice.GrandTotal.ToCurrency()%>
                    </td>
                     <td class="col col-15 col5 cold">
						<%=invoice.GrandTotalWithTax.ToCurrency()%>
                    </td>                   
              </tr>
            <% } %>
        </tbody>
        
       </table> 
        <%Html.ShowPager(Model); %>
        </div>
	</div>
</div>
</asp:Content>

