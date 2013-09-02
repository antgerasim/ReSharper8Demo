<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<QuoteList>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Liste des devis</title>
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
              <span>
                   Votre compte personnalisé : 
              </span>
          </h2>
        <div class="chemin">
        <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref() %>">Tableau de bord</a></span>
        <b>
		<% if (Request["StatusId"] == "0") { %>
            <% if (Model.ItemCount == 0) { %>
            Il n'y a pas encore de devis
            <% } else { %> 
            Tous les devis (<%=Model.ItemCount%>)
            <% } %> 
        <% } else if (Request["StatusId"] == "4") { %>
            <% if (Model.ItemCount == 0) { %>
            Il n'y a pas de devis converti en commande
            <% } else {  %> 
            Les <%=Model.ItemCount%> devis convertis en commande
            <% } %> 
        <% } else { %>
            <% if (Model.ItemCount == 0) { %>
            Pas de devis en cours
            <% } else {  %> 
            Les <%=Model.ItemCount%> devis en cours
            <% } %> 
        <% } %> 
        </b>
        </div> 
        <div class="ssgrid321">
	
			<%Html.ShowPager(Model); %>
        
            <%Html.RenderPartial("~/views/account/quote/quotelist.ascx", Model);%>
        
            <%Html.ShowPager(Model); %>
        </div>

	 </div>
</div>
</asp:Content>
