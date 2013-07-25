<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<OrderList>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Liste des commandes</title>
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
             <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref() %>">Tableau de bord</a></span><b>Commandes</b>
             <b>
            <%if (Model.IsNullOrEmpty()) { %>
                Vous n'avez passé aucune commande à ce jour
            <% } else if (Model.IsNullOrEmpty()) { %>
                Pas de commande
            <% } %>
            </b>
         </div>

	<div class="ssgrid321">
        
	<%Html.ShowPager(Model);%>
	
    <ul class="onglets">
         <li><a href="<%=Url.AddParameter("StatusId","0")%>" class="corner">Toutes les commandes</a></li>
         <li><a href="<%=Url.AddParameter("StatusId","7")%>" class="corner">Les commandes en cours</a></li>
    </ul> 
  
          <table class="nav cols">
                <thead>
                    <tr class="entete-cols">
                        <th class="col col-25 col1">Code</th>
                        <th class="col col-20 col2">Statut</th>
                        <th class="col col-25 col3">Création</th>
                        <th class="col col-15 col4">Nb. de produits</th>
                        <th class="col col-15 col5">total HT</th> 
                    </tr>
                </thead>
 
                <tbody>
				<% foreach (var order in Model) { %>
                 <tr class="<%=Model.ColumnIndexName(order, 2, "prodligne")%>">
                    <td class="col col-25 col1">
						Code : <a href="<%=Url.Href(order)%>" title="voir le détail"><%=order.Code%></a></strong><br/><small>(ref : <%=order.CustomerDocumentReference%>) </small>
                    </td>        
                    <td class="col col-20 col2">
                        <%=order.OrderStatus.GetLocalizedName()%>
                    </td>
                    <td class="col col-25 col3 cold">
                        <%=string.Format("{0:dddd dd MMMM yyyy}", order.CreationDate)%>
                    </td>
                    <td class="col col-15 col4 form_element_radio">
                         <%=order.ItemCount%>
                    </td>
                    <td class="col col-15 col5 cold">
						<%=order.GrandTotalWithTax.ToCurrency()%>
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
      
        <%Html.ShowPager(Model);%>
    
    	</div>

	</div>
</div>
</asp:Content>